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
			int pawnDirection = side == white ? -boardWidth : boardWidth;
			if (board[square + pawnDirection + 1] == side + pawnBit || board[square + pawnDirection - 1] == side + pawnBit)
				return true;

			foreach (int dir in kingDirections)
				if (board[square + dir] == side + kingBits) return true;

			foreach (int dir in knightDirections)
				if (board[square + dir] == side + knightBit) return true;
			
			foreach (int dir in orthogonalDirections)
			{
				int temporarySquare = square + dir;
				while (board[temporarySquare] == empty)
					temporarySquare += dir;
				if ((board[temporarySquare] & (orthogonalBit + border)) == side + orthogonalBit)
					return true; 
			}
			
			foreach (int dir in diagionalDirections)
			{ 
				int temporarySquare = square + dir;
				while (board[temporarySquare] == empty)
					temporarySquare += dir;
				if ((board[temporarySquare] & (diagionalBit + border)) == side + diagionalBit)
					return true;
			}

			return false;
		}
	}
}
