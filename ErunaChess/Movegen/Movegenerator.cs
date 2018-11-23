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
			{ Global.blackKnight, knightDirections },
			{ Global.whiteKing, kingDirections },
			{ Global.blackKing, kingDirections },
			{ Global.blackRook, rookDirections },
			{ Global.whiteRook, rookDirections },
			{ Global.whiteBishop, bishopDirections },
			{ Global.blackBishop, bishopDirections },
			{ Global.whiteQueen, kingDirections },
			{ Global.blackQueen, kingDirections }
		};

		public static int enemy;

		public static void GenerateMovesForPiece(Board board, MovesList movesList, int piece, int directions, bool slider)
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
								AddMove.CaptureMove(board, Move.Write(square, toSquare, board.board[toSquare], 0, false, false, false), movesList);
							}
							break;
						}
						AddMove.QuietMove(board, Move.Write(square, toSquare, 0, 0, false, false, false), movesList);
						toSquare += Directions[piece][j];
					} while (slider);
				}
			}
		}

		public static void GeneratePawnMoves(Board board, MovesList movesList)
		{
			int piece = board.side == Global.white ? Global.whitePawn : Global.blackPawn;
			int direction = board.side == Global.white ? Global.boardWidth : -Global.boardWidth;

			for (int i = 0; i < board.pieces[piece].Count; i++)
			{
				int square = board.pieces[piece][i];
				// promotions
				if (board.side == Global.white ? square >= (int)Global.Square.A7 : square <= (int)Global.Square.H2)
				{
					if (board.board[square + direction] == Global.empty)
						AddMove.PromotionMove(board, Move.Write(square, square + direction, 0, 0, false, false, false), movesList);
					//captures
					if ((board.board[square + direction + 1] & Global.border) == enemy)
						AddMove.PromotionMove(board, Move.Write(square, square + direction + 1, board.board[square + direction + 1], 0, false, false, false), movesList);

					if ((board.board[square + direction - 1] & Global.border) == enemy)
						AddMove.PromotionMove(board, Move.Write(square, square + direction - 1, board.board[square + direction - 1], 0, false, false, false), movesList);
				}
				else
				{
					if (board.board[square + direction] == Global.empty)
					{
						AddMove.QuietPawnMove(board, Move.Write(square, square + direction, 0, 0, false, false, false), movesList);
						//pawnstart
						if ((board.side == Global.white ? (square <= (int)Global.Square.H2) : (square >= (int)Global.Square.A7)) && (board.board[square + direction * 2] == Global.empty)) 
							AddMove.QuietPawnMove(board, Move.Write(square, square + direction * 2, 0, 0, false, true, false), movesList);
					}
					//captures
					if ((board.board[square + direction + 1] & Global.border) == enemy)
						AddMove.PawnCaptureMove(board, Move.Write(square, square + direction + 1, board.board[square + direction + 1], 0, false, false, false), movesList);

					if ((board.board[square + direction - 1] & Global.border) == enemy)
						AddMove.PawnCaptureMove(board, Move.Write(square, square + direction - 1, board.board[square + direction - 1], 0, false, false, false), movesList);
					//enpassant
					if (square + direction + 1 == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board.board[board.enpassantSquare], 0, true, false, false), movesList);

					if (square + direction - 1 == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board.board[board.enpassantSquare], 0, true, false, false), movesList);
				}
			}
		}

		public static void GenerateCastlingMoves(Board board, MovesList movesList)
		{
			int kingSideCastle = board.side == Global.white ? Global.whiteKingSideCastle : Global.blackKingSideCastle;
			int queenSideCastle = board.side == Global.white ? Global.whiteQueenSideCastle : Global.blackQueenSideCastle;
			int kingSquare = board.pieces[board.side + Global.kingBits][0];

			if ((board.castlePermission & kingSideCastle) > 0)
			{
				if (board.board[kingSquare + 1] == Global.empty && board.board[kingSquare + 2] == Global.empty)
				{
					if (!Attack.SquareAttacked(board, kingSquare, enemy) && !Attack.SquareAttacked(board, kingSquare + 1, enemy))
						AddMove.QuietMove(board, Move.Write(kingSquare, kingSquare + 2, 0, 0, false, false, true), movesList);
				}
			}

			if ((board.castlePermission & queenSideCastle) > 0)
			{
				if (board.board[kingSquare - 1] == Global.empty && board.board[kingSquare - 2] == Global.empty && board.board[kingSquare - 3] == Global.empty)
				{
					if (!Attack.SquareAttacked(board, kingSquare, enemy) && !Attack.SquareAttacked(board, kingSquare -1, enemy))
						AddMove.QuietMove(board, Move.Write(kingSquare, kingSquare - 2, 0, 0, false, false, true), movesList);
				}
			}
		}

		public static void GenerateAllMoves(Board board, MovesList movesList)
		{
			enemy = board.side ^ Global.border;

			GeneratePawnMoves(board, movesList);

			GenerateCastlingMoves(board, movesList);

			GenerateMovesForPiece(board, movesList, board.side == Global.white ? Global.whiteKnight : Global.blackKnight, 8 , false);

			GenerateMovesForPiece(board, movesList, board.side == Global.white ? Global.whiteKing : Global.blackKing, 8, false);

			GenerateMovesForPiece(board, movesList, board.side == Global.white ? Global.whiteQueen : Global.blackQueen, 8, true);

			GenerateMovesForPiece(board, movesList, board.side == Global.white ? Global.whiteRook : Global.blackRook, 4, true);

			GenerateMovesForPiece(board, movesList, board.side == Global.white ? Global.whiteBishop : Global.blackBishop, 4, true);
		}
	}
}
