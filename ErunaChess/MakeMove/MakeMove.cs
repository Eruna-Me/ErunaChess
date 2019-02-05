using static ErunaChess.Global;
using static ErunaChess.Global.Square;

namespace ErunaChess
{
	static class MakeMove
	{
		static readonly int[] castleBoard = new int[boardSize];

		public static void InitCastleBoard()
		{
			for(int i = 0; i < boardSize; i++)
			{
				castleBoard[i] = 15;
			}
			castleBoard[(int)A1] -= whiteQueenSideCastle;
			castleBoard[(int)H1] -= whiteKingSideCastle;
			castleBoard[(int)E1] += -whiteQueenSideCastle - whiteKingSideCastle;
			castleBoard[(int)A8] -= blackQueenSideCastle;
			castleBoard[(int)E8] += -blackKingSideCastle - blackQueenSideCastle;
			castleBoard[(int)H8] -= blackKingSideCastle;
		}

		static void ClearPiece(Board board, int square)
		{ 
			board.pieces[board[square]].Remove(square);

			board[square] = empty;
		}

		static void AddPiece(Board board, int square, int piece)
		{
			board[square] = piece;

			board.pieces[piece].Add(square);
		}

		static void MovePiece(Board board, int from, int to)
		{
			int piece = board[from];

			board[from] = empty;

			board[to] = piece;

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
			int piece = board[from];

			if ((move & Move.EnPassantFlag()) > 0)
				ClearPiece(board, board.side == white ? to - boardWidth : to + boardWidth);

			if ((move & Move.CastleFlag()) > 0)
			{
				switch (to)
				{
					case (int)C1: MovePiece(board, (int)A1, (int)D1); break;
					case (int)C8: MovePiece(board, (int)A8, (int)D8); break;
					case (int)G1: MovePiece(board, (int)H1, (int)F1); break;
					case (int)G8: MovePiece(board, (int)H8, (int)F8); break;
				}
			}

			board.history[board.historyPly].move = move;
			board.history[board.historyPly].fiftymove = board.fiftyMove;
			board.history[board.historyPly].enpassantSquare = board.enpassantSquare;
			board.history[board.historyPly].castlePermission = board.castlePermission;

			board.castlePermission &= castleBoard[to];
			board.castlePermission &= castleBoard[from];
			board.enpassantSquare = (int)offBoard;

			int captured = Move.Captured(move);
			board.fiftyMove++;

			if(captured != empty)
			{
				ClearPiece(board, to);
				board.fiftyMove = 0;
			}

			board.ply++;
			board.historyPly++;

			if ((piece & pawnBit) > 0)
			{
				if ((move & Move.PawnStartFlag()) > 0)
				{
					if (board.side == white)
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

			if (promoted != empty)
			{
				ClearPiece(board, to);
				AddPiece(board, to, promoted);
			}

			board.side ^= border;

			if (Attack.SquareAttacked(board, board.pieces[kingBits + (board.side ^ border)][0], board.side))
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

			board.side ^= border;

			if ((move & Move.EnPassantFlag()) > 0)
			{
				if (board.side == white)
				{
					AddPiece(board, to - 16 , blackPawn);
				}
				else
				{
					AddPiece(board, to + 16, whitePawn);
				}
			}

			if((Move.CastleFlag() & move) != 0)
			{
				switch(to)
				{
					case (int)C1: MovePiece(board, (int)D1, (int)A1); break;
					case (int)C8: MovePiece(board, (int)D8, (int)A8); break;
					case (int)G1: MovePiece(board, (int)F1, (int)H1); break;
					case (int)G8: MovePiece(board, (int)F8, (int)H8); break;
				}
			}

			MovePiece(board, to, from);

			int capturedPiece = Move.Captured(move);

			if (capturedPiece != empty)
			{
				AddPiece(board, to, capturedPiece);
			}

			if (Move.Promoted(move) != empty)
			{
				ClearPiece(board, from);
				AddPiece(board, from, board.side + pawnBit);
			}
		}
	}
}
