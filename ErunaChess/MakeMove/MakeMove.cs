using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	static class MakeMove
	{
		static readonly int[] castleBoard = new int[Global.boardSize];

		public static void InitCastleBoard()
		{
			for(int i = 0; i < Global.boardSize; i++)
			{
				castleBoard[i] = 15;
			}
			castleBoard[(int)Global.Square.A1] -= Global.whiteQueenSideCastle;
			castleBoard[(int)Global.Square.H1] -= Global.whiteKingSideCastle;
			castleBoard[(int)Global.Square.E1] += -Global.whiteQueenSideCastle - Global.whiteKingSideCastle;
			castleBoard[(int)Global.Square.A8] -= Global.blackQueenSideCastle;
			castleBoard[(int)Global.Square.E8] += -Global.blackKingSideCastle - Global.blackQueenSideCastle;
			castleBoard[(int)Global.Square.H8] -= Global.blackKingSideCastle;
		}

		static void ClearPiece(Board board, int square)
		{
			int piece = board.board[square];

			board.board[square] = Global.empty;

			for(int i = 0; i < board.pieces[piece].Count; i++)
			{
				if(board.pieces[piece][i] == square)
				{
					board.pieces[piece].RemoveAt(i);
					break;
				}
			}
		}

		static void AddPiece(Board board, int square, int piece)
		{
			board.board[square] = piece;

			board.pieces[piece].Add(square);
		}

		static void MovePiece(Board board, int from, int to)
		{
			int piece = board.board[from];

			board.board[from] = Global.empty;

			board.board[to] = piece;
			
			for (int i = 0; i < board.pieces[piece].Count; i++)
			{
				if (board.pieces[piece][i] == from)
				{
					board.pieces[piece][i] = to;
					break;
				}
			} 
		}

		public static bool Make(Board board, int move)
		{
			int from = Move.From(move);
			int to = Move.To(move); 
			int piece = board.board[from];

			if ((move & Move.EnPassantFlag()) > 0)
				ClearPiece(board, board.side == Global.white ? to - Global.boardWidth : to + Global.boardWidth);

			if ((move & Move.CastleFlag()) > 0)
			{
				switch (to)
				{
					case (int)Global.Square.C1: MovePiece(board, (int)Global.Square.A1, (int)Global.Square.D1); break;
					case (int)Global.Square.C8: MovePiece(board, (int)Global.Square.A8, (int)Global.Square.D8); break;
					case (int)Global.Square.G1: MovePiece(board, (int)Global.Square.H1, (int)Global.Square.F1); break;
					case (int)Global.Square.G8: MovePiece(board, (int)Global.Square.H8, (int)Global.Square.F8); break;
				}
			}

			board.history[board.historyPly].move = move;
			board.history[board.historyPly].fiftymove = board.fiftyMove;
			board.history[board.historyPly].enpassantSquare = board.enpassantSquare;
			board.history[board.historyPly].castlePermission = board.castlePermission;

			board.castlePermission &= castleBoard[to];
			board.castlePermission &= castleBoard[from];
			board.enpassantSquare = (int)Global.Square.offBoard;

			int captured = Move.Captured(move);
			board.fiftyMove++;

			if(captured != Global.empty)
			{
				ClearPiece(board, to);
				board.fiftyMove = 0;
			}

			board.ply++;
			board.historyPly++;

			if ((piece & Global.pawnBit) > 0)
			{
				if ((move & Move.PawnStartFlag()) > 0)
				{
					if (board.side == Global.white)
					{
						board.enpassantSquare = from + 16;
					}
					else
					{
						board.enpassantSquare = from - 16;
					}
				}
				board.fiftyMove = 0;
			}

			MovePiece(board, from, to);

			int promoted = Move.Promoted(move);

			if (promoted != Global.empty)
			{
				ClearPiece(board, to);
				AddPiece(board, to, promoted);
			}

			board.side ^= Global.border;

			if (Attack.SquareAttacked(board, board.pieces[Global.kingBits + (board.side ^ Global.border)][0], board.side))
			{
				Take(board);
				return false;
			}

			return true;
		}

		public static void Take (Board board)
		{
			board.historyPly--;
			board.ply--;

			int move = board.history[board.historyPly].move;
			int from = Move.From(move);
			int to = Move.To(move);

			board.castlePermission = board.history[board.historyPly].castlePermission;
			board.fiftyMove = board.history[board.historyPly].fiftymove;
			board.enpassantSquare = board.history[board.historyPly].enpassantSquare;

			board.side ^= Global.border;

			if ((move & Move.EnPassantFlag()) > 0)
			{
				if (board.side == Global.white)
				{
					AddPiece(board, to - 16 , Global.blackPawn);
				}
				else
				{
					AddPiece(board, to + 16, Global.whitePawn);
				}
			}

			if((Move.CastleFlag() & move) != 0)
			{
				switch(to)
				{
					case (int)Global.Square.C1: MovePiece(board, (int)Global.Square.D1, (int)Global.Square.A1); break;
					case (int)Global.Square.C8: MovePiece(board, (int)Global.Square.D8, (int)Global.Square.A8); break;
					case (int)Global.Square.G1: MovePiece(board, (int)Global.Square.F1, (int)Global.Square.H1); break;
					case (int)Global.Square.G8: MovePiece(board, (int)Global.Square.F8, (int)Global.Square.H8); break;
				}
			}

			MovePiece(board, to, from);

			int capturedPiece = Move.Captured(move);

			if (capturedPiece != Global.empty)
			{
				AddPiece(board, to, capturedPiece);
			}

			if (Move.Promoted(move) != Global.empty)
			{
				ClearPiece(board, from);
				AddPiece(board, from, board.side + Global.pawnBit);
			}
		}
	}
}
