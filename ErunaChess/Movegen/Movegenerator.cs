using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public static class MoveGenerator
	{
		static readonly int[] knightDirections = { 33, 31, -33, -31, 18, 14, -18, -14 };
		static readonly int[] kingDirections = { 1, -1, 16, 15, 17, -16, -15, -17 };
		static readonly int[] rookDirections = { 1, -1, 16, -16 };
		static readonly int[] bishopDirections = { 15, 17, -15, -17 };
		static readonly Dictionary<int, int[]> Directions = new Dictionary<int, int[]>()
		{
			{ Global.whiteKnight, knightDirections },
			{ Global.blackKnight, knightDirections},
			{ Global.whiteKing, kingDirections},
			{ Global.blackKing, kingDirections },
			{ Global.blackRook, rookDirections },
			{ Global.whiteRook, rookDirections },
			{ Global.whiteBishop, bishopDirections},
			{ Global.blackBishop, bishopDirections },
			{ Global.whiteQueen, kingDirections },
			{ Global.blackQueen, kingDirections }
		};

		public static int enemy;

		public static void GenerateMovesForPiece(Board board, MovesList moveList, int piece, int directions, bool slider)
		{
			for (int i = 0; i < board.pieces[piece].Count; i++) 
			{
				int square = board.pieces[piece][i];
				for (int j = 0; j < directions; j++)
				{
					int toSquare = square + Directions[piece][j];
					do
					{
						if ((board.board[toSquare] & Global.border) > 0)
						{
							if ((board.board[toSquare] & Global.border) == enemy)
							{
								AddMove.CaptureMove(board, Move.Write(square, toSquare, board.board[toSquare], 0, false, false, false), moveList);
							}
							break;
						}
						AddMove.QuietMove(board, Move.Write(square, toSquare, 0, 0, false, false, false), moveList);
						toSquare += Directions[piece][j];
					} while (slider);
				}
			}
		}

		public static void GenerateAllMoves(Board board, MovesList moveList)
		{
			enemy = board.side ^ Global.border;
			
			// generate white pawn moves
			if (board.side == Global.white)
			{
				for (int i = 0; i < board.pieces[Global.whitePawn].Count; i++)
				{
					int square = board.pieces[Global.whitePawn][i];
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
				//castling
				if ((board.castlePermission & Global.whiteKingSideCastle) > 0)
				{
					if (board.board[73] == Global.empty && board.board[74] == Global.empty)
					{
						if (Attack.SquareAttacked(enemy, 73, board) && Attack.SquareAttacked(enemy, 74, board))
							AddMove.QuietMove(board, Move.Write(72, 74, 0, 0, false, false, true), moveList);
					}
				}

				if ((board.castlePermission & Global.whiteQueenSideCastle) > 0)
				{
					if (board.board[71] == Global.empty && board.board[70] == Global.empty)
					{
						if (Attack.SquareAttacked(enemy, 71, board) && Attack.SquareAttacked(enemy, 70, board))
							AddMove.QuietMove(board, Move.Write(72, 70, 0, 0, false, false, true), moveList);
					}
				}
			}
			// generate black pawn moves
			else
			{
				for (int i = 0; i < board.pieces[Global.blackPawn].Count; i++)
				{
					int square = board.pieces[Global.blackPawn][i];
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
				//castling
				if ((board.castlePermission & Global.blackKingSideCastle) > 0)
				{
					if (board.board[185] == Global.empty && board.board[186] == Global.empty)
					{
						if (Attack.SquareAttacked(enemy, 185, board) && Attack.SquareAttacked(enemy, 186, board))
							AddMove.QuietMove(board, Move.Write(184, 186, 0, 0, false, false, true), moveList);
					}
				}

				if ((board.castlePermission & Global.blackQueenSideCastle) > 0)
				{
					if (board.board[183] == Global.empty && board.board[182] == Global.empty)
					{
						if (Attack.SquareAttacked(enemy, 183, board) && Attack.SquareAttacked(enemy, 182, board))
							AddMove.QuietMove(board, Move.Write(184, 182, 0, 0, false, false, true), moveList);
					}
				}
			} 

			GenerateMovesForPiece(board, moveList, board.side == Global.white ? Global.whiteKnight : Global.blackKnight, 8 , false);

			GenerateMovesForPiece(board, moveList, board.side == Global.white ? Global.whiteKing : Global.blackKing, 8, false);

			GenerateMovesForPiece(board, moveList, board.side == Global.white ? Global.whiteQueen : Global.blackQueen, 8, true);

			GenerateMovesForPiece(board, moveList, board.side == Global.white ? Global.whiteRook : Global.blackRook, 4, true);

			GenerateMovesForPiece(board, moveList, board.side == Global.white ? Global.whiteBishop : Global.blackBishop, 4, true);

			Console.WriteLine(moveList.moves.Count + " moves"); //Temporary debugging code!!!

			for (int i = 0; i < moveList.moves.Count; i++ )
			{
				Console.WriteLine();
				Console.WriteLine(Move.From(moveList.moves[i].move) + " from");
				Console.WriteLine(Move.To(moveList.moves[i].move) + " to");
			}
		}
	}
}
