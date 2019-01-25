using static ErunaChess.Global;

namespace ErunaChess
{
	class Attack
	{
		static readonly int[] knightDirections = { 31, 33, 14, 18, -31, -33, -14, -18 };
		static readonly int[] kingDirections = { 15, 16, 17, -1, 1, -15, -16, -17 };
		static readonly int[] orthogonalDirections = { 16, -16, 1, -1 };
		static readonly int[] diagionalDirections = { 15, 17, -15, -17 };

		public static bool SquareAttacked(Board board, int square, int side)
		{
			// Pawns
			int pawnDirection = side == white ? -boardWidth : boardWidth;
			if (board[square + pawnDirection + 1] == side + pawnBit || board[square + pawnDirection - 1] == side + pawnBit)
				return true;

			//king
			for (int i = 0; i < 8; i++)
				if (board[square + kingDirections[i]] == (side == white ? whiteKing : blackKing)) return true;

			//Knights
			for (int i = 0; i < 8; i++)
				if (board[square + knightDirections[i]] == (side == white ? whiteKnight : blackKnight)) return true;
			
			//sliders
			for (int i = 0; i < 4; i++)
			{
				int direction = orthogonalDirections[i];
				int temporarySquare = square + direction;
				while (board[temporarySquare] == empty)
					temporarySquare += direction;
				if ((board[temporarySquare] & (orthogonalBit + border)) == (side == white ? whiteRook : blackRook))
					return true; 
			}
			
			for (int i = 0; i < 4; i++)
			{
				int direction = diagionalDirections[i];
				int temporarySquare = square + direction;
				while (board[temporarySquare] == empty)
					temporarySquare += direction;
				if ((board[temporarySquare] & (diagionalBit + border)) == (side == white ? whiteBishop : blackBishop))
					return true;
			}

			return false;
		}
	}
}
