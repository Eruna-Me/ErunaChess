using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public static class MoveGenerator
	{
		static readonly int[] KnightDirections = { 33, 31, -33, -31, 18, 14, -18, -14 };
		static readonly int[] KingDirections = { 1, -1, 16, 15, 17, -16, -15, -17 };

		public static void GenerateAllMoves(Board board, MoveList moveList)
		{
			int enemy = board.side ^ Global.border;
			
			// generate white pawn moves
			if (board.side == Global.white)
			{
				for (int i = 0; i < board.pieceCount[(int)Board.Pieces.whitePawn]; i++)
				{
					int square = board.pieces[(int)Board.Pieces.whitePawn, i];
					// if 7th rank, promotion fun.
					if (square >= (int)Global.Square.A7)
					{
						if (board.board[square + Global.boardWidth] == Global.empty)	
							AddMove.PromotionMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, false, false), moveList);
						//captures
						if ((board.board[square + Global.boardWidth + 1] & Global.border) == enemy)
							AddMove.PromotionMove(board, Move.Write(square, square + Global.boardWidth + 1, board.board[square + Global.boardWidth + 1], 0, false, false, false), moveList);

						if ((board.board[square + Global.boardWidth - 1] & Global.border) == enemy)
							AddMove.PromotionMove(board, Move.Write(square, square + Global.boardWidth - 1, board.board[square + Global.boardWidth - 1], 0, false, false, false), moveList);
					}
					else
					{
						if (board.board[square + Global.boardWidth] == Global.empty)
						{
							AddMove.QuietPawnMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, false, false), moveList);

							if (square <= (int)Global.Square.H2 && board.board[square + Global.boardWidth * 2] == Global.empty)
								AddMove.QuietPawnMove(board, Move.Write(square, square + Global.boardWidth * 2, 0, 0, false, true, false), moveList);
						}
						//captures
						if ((board.board[square + Global.boardWidth + 1] & Global.border) == enemy)
							AddMove.PawnCaptureMove(board, Move.Write(square, square + Global.boardWidth + 1, board.board[square + Global.boardWidth + 1], 0, false, false, false), moveList);

						if ((board.board[square + Global.boardWidth - 1] & Global.border) == enemy)
							AddMove.PawnCaptureMove(board, Move.Write(square, square + Global.boardWidth - 1, board.board[square + Global.boardWidth - 1], 0, false, false, false), moveList);
						//enpassant
						if (square + Global.boardWidth + 1 == board.enpassantSquare)
							AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board.board[board.enpassantSquare], 0, true, false, false), moveList);

						if (square + Global.boardWidth - 1 == board.enpassantSquare)
							AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board.board[board.enpassantSquare], 0, true, false, false), moveList);
					}
				}
			}
			// generate black pawn moves
			else
			{
				for (int i = 0; i < board.pieceCount[(int)Board.Pieces.blackPawn]; i++)
				{
					int square = board.pieces[(int)Board.Pieces.blackPawn, i];
					// if 2nd rank, promotion fun.
					if ( square <= (int)Global.Square.H2)
					{
						if (board.board[square - Global.boardWidth] == Global.empty)
							AddMove.PromotionMove(board, Move.Write(square, square - Global.boardWidth, 0, 0, false, false, false), moveList);
						//captures
						if ((board.board[square - Global.boardWidth + 1] & Global.border) == enemy)
							AddMove.PromotionMove(board, Move.Write(square, square - Global.boardWidth + 1, board.board[square - Global.boardWidth + 1], 0, false, false, false), moveList);

						if ((board.board[square - Global.boardWidth - 1] & Global.border) == enemy)
							AddMove.PromotionMove(board, Move.Write(square, square - Global.boardWidth - 1, board.board[square - Global.boardWidth - 1], 0, false, false, false), moveList);
					}
					else
					{
						if (board.board[square - Global.boardWidth] == Global.empty)
						{
							AddMove.QuietPawnMove(board, Move.Write(square, square - Global.boardWidth, 0, 0, false, false, false), moveList);

							if (square >= (int)Global.Square.A7 && board.board[square - Global.boardWidth * 2] == Global.empty)
								AddMove.QuietPawnMove(board, Move.Write(square, square - Global.boardWidth * 2, 0, 0, false, true, false), moveList);
						}
						//captures
						if ((board.board[square - Global.boardWidth + 1] & Global.border) == enemy)
							AddMove.PawnCaptureMove(board, Move.Write(square, square - Global.boardWidth + 1, board.board[square - Global.boardWidth + 1], 0, false, false, false), moveList);

						if ((board.board[square - Global.boardWidth - 1] & Global.border) == enemy)
							AddMove.PawnCaptureMove(board, Move.Write(square, square - Global.boardWidth - 1, board.board[square - Global.boardWidth - 1], 0, false, false, false), moveList);
						//enpassant
						if (square - Global.boardWidth + 1 == board.enpassantSquare)
							AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board.board[board.enpassantSquare], 0, true, false, false), moveList);

						if (square - Global.boardWidth - 1 == board.enpassantSquare)
							AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board.board[board.enpassantSquare], 0, true, false, false), moveList);
					}
				}
			} 

			int knight = board.side == Global.white ? (int)Board.Pieces.whiteKnight : (int)Board.Pieces.blackKnight;
			for (int i = 0; i < board.pieceCount[knight]; i++)
			{
				int square = board.pieces[knight, i];
				for (int j = 0; j < 8; j++)
				{
					int toSquare = square + KnightDirections[j];
					if (board.board[toSquare] == Global.empty)
					{
						AddMove.QuietMove(board, Move.Write(square, toSquare, 0, 0, false, false, false), moveList);
					}
					if ((board.board[toSquare] & Global.border) == enemy)
					{
						AddMove.CaptureMove(board, Move.Write(square, toSquare, board.board[toSquare], 0, false, false, false), moveList);
					}
				}
			}

			int king = board.side == Global.white ? (int)Board.Pieces.whiteKing : (int)Board.Pieces.blackKing;
			for (int i = 0; i < board.pieceCount[king]; i++)
			{
				int square = board.pieces[king, i];
				for (int j = 0; j < 8; j++)
				{
					int toSquare = square + KingDirections[j];
					if (board.board[toSquare] == Global.empty)
					{
						AddMove.QuietMove(board, Move.Write(square, toSquare, 0, 0, false, false, false), moveList);
					}
					if ((board.board[toSquare] & Global.border) == enemy)
					{
						AddMove.CaptureMove(board, Move.Write(square, toSquare, board.board[toSquare], 0, false, false, false), moveList);
					}
				}
			}

			Console.WriteLine(moveList.count + " moves");

			for (int i = 0; i < moveList.count; i++ )
			{
				Console.WriteLine();
				Console.WriteLine(Move.From(moveList.moves[i].move) + " from");
				Console.WriteLine(Move.To(moveList.moves[i].move) + " to");
			}
		}
	}
}
