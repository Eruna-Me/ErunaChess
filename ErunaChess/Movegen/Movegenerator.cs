using static ErunaChess.Global.Square;
using static ErunaChess.Global;

namespace ErunaChess
{
	public static class MoveGenerator
	{
		static public void InitMoveGenerator()
		{
			Directions[whiteKnight] = knightDirections;
			Directions[whiteBishop] = bishopDirections;
			Directions[whiteRook] = rookDirections;
			Directions[whiteQueen] = kingDirections;
			Directions[whiteKing] = kingDirections;
			Directions[blackKnight] = knightDirections;
			Directions[blackBishop] = bishopDirections;
			Directions[blackRook] = rookDirections;
			Directions[blackQueen] = kingDirections;
			Directions[blackKing] = kingDirections;
		}

		static readonly int[] knightDirections = { 33, 31, -33, -31, 18, 14, -18, -14 };
		static readonly int[] kingDirections = { 1, -1, 16, 15, 17, -16, -15, -17 };
		static readonly int[] rookDirections = { 1, -1, 16, -16 };
		static readonly int[] bishopDirections = { 15, 17, -15, -17 };
		static readonly int[][] Directions = new int [border][];

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
						if ((board[toSquare] & border) > 0)
						{
							if ((board[toSquare] & border) == enemy)
							{
								AddMove.CaptureMove(board, Move.Write(square, toSquare, board[toSquare], 0, false, false, false), movesList);
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
			int piece = board.side == white ? whitePawn : blackPawn;
			int direction = board.side == white ? boardWidth : -boardWidth;

			for (int i = 0; i < board.pieces[piece].Count; i++)
			{
				int square = board.pieces[piece][i];
				// promotions
				if (board.side == white ? square >= (int)A7 : square <= (int)H2)
				{
					if (board[square + direction] == empty)
						AddMove.PromotionMove(board, Move.Write(square, square + direction, 0, 0, false, false, false), movesList);
					//captures
					if ((board[square + direction + 1] & border) == enemy)
						AddMove.PromotionMove(board, Move.Write(square, square + direction + 1, board[square + direction + 1], 0, false, false, false), movesList);

					if ((board[square + direction - 1] & border) == enemy)
						AddMove.PromotionMove(board, Move.Write(square, square + direction - 1, board[square + direction - 1], 0, false, false, false), movesList);
				}
				else
				{
					if (board[square + direction] == empty)
					{
						AddMove.QuietPawnMove(board, Move.Write(square, square + direction, 0, 0, false, false, false), movesList);
						//pawnstart
						if ((board.side == white ? (square <= (int)H2) : (square >= (int)A7)) && (board[square + direction * 2] == empty)) 
							AddMove.QuietPawnMove(board, Move.Write(square, square + direction * 2, 0, 0, false, true, false), movesList);
					}
					//captures
					if ((board[square + direction + 1] & border) == enemy)
						AddMove.PawnCaptureMove(board, Move.Write(square, square + direction + 1, board[square + direction + 1], 0, false, false, false), movesList);

					if ((board[square + direction - 1] & border) == enemy)
						AddMove.PawnCaptureMove(board, Move.Write(square, square + direction - 1, board[square + direction - 1], 0, false, false, false), movesList);
					//enpassant
					if (square + direction + 1 == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board[board.enpassantSquare], 0, true, false, false), movesList);

					if (square + direction - 1 == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.enpassantSquare, board[board.enpassantSquare], 0, true, false, false), movesList);
				}
			}
		}

		public static void GenerateCastlingMoves(Board board, MovesList movesList)
		{
			int kingSideCastle = board.side == white ? whiteKingSideCastle : blackKingSideCastle;
			int queenSideCastle = board.side == white ? whiteQueenSideCastle : blackQueenSideCastle;
			int kingSquare = board.pieces[board.side + kingBits][0];

			if ((board.castlePermission & kingSideCastle) > 0)
			{
				if (board[kingSquare + 1] == empty && board[kingSquare + 2] == empty)
				{
					if (!Attack.SquareAttacked(board, kingSquare, enemy) && !Attack.SquareAttacked(board, kingSquare + 1, enemy))
						AddMove.QuietMove(board, Move.Write(kingSquare, kingSquare + 2, 0, 0, false, false, true), movesList);
				}
			}

			if ((board.castlePermission & queenSideCastle) > 0)
			{
				if (board[kingSquare - 1] == empty && board[kingSquare - 2] == empty && board[kingSquare - 3] == empty)
				{
					if (!Attack.SquareAttacked(board, kingSquare, enemy) && !Attack.SquareAttacked(board, kingSquare -1, enemy))
						AddMove.QuietMove(board, Move.Write(kingSquare, kingSquare - 2, 0, 0, false, false, true), movesList);
				}
			}
		}

		public static void GenerateAllMoves(Board board, MovesList movesList)
		{
			enemy = board.side ^ border;

			GeneratePawnMoves(board, movesList);

			GenerateCastlingMoves(board, movesList);

			GenerateMovesForPiece(board, movesList, board.side == white ? whiteKnight : blackKnight, 8 , false);

			GenerateMovesForPiece(board, movesList, board.side == white ? whiteKing : blackKing, 8, false);

			GenerateMovesForPiece(board, movesList, board.side == white ? whiteQueen : blackQueen, 8, true);

			GenerateMovesForPiece(board, movesList, board.side == white ? whiteRook : blackRook, 4, true);

			GenerateMovesForPiece(board, movesList, board.side == white ? whiteBishop : blackBishop, 4, true);
		}
	}
}
