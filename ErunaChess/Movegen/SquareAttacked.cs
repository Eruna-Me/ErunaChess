using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	class Attack
	{
		static readonly int[] knightDirections = { 31, 33, 14, 18, -31, -33, -14, -18 };
		static readonly int[] kingDirections = { 15, 16, 17, -1, 1, -15, -16, -17 };
		static readonly int[] orthogonalDirections = { 16, -16, 1, -1 };
		static readonly int[] diagionalDirections = { 15, 17, -15, -17 };

		public static bool SquareAttacked(int side, int square, Board board)
		{
			// Pawns
			if (side == Global.white)
			{
				if (board.board[square - 15] == Global.whitePawn || board.board[square -17] == Global.whitePawn)
					return true;
			}
			else
			{
				if (board.board[square + 15] == Global.blackPawn || board.board[square + 17] == Global.blackPawn)
					return true;
			}

			//king
			for (int i = 0; i < 8; i++)
				if (board.board[square + kingDirections[i]] == (side == Global.white ? Global.whiteKing : Global.blackKing)) return true;

			//Knights
			for (int i = 0; i < 8; i++)
				if (board.board[square + knightDirections[i]] == (side == Global.white ? Global.whiteKnight : Global.blackKnight)) return true;
			
			//sliders
			for (int i = 0; i < 4; i++)
			{
				int direction = orthogonalDirections[i];
				int temporarySquare = square + direction;
				while (board.board[temporarySquare] == Global.empty)
					temporarySquare += direction;
				if ((board.board[temporarySquare] & (Global.orthogonalBit + Global.border)) == (side == Global.white ? Global.whiteRook : Global.blackRook))
					return true; 
			}
			
			for (int i = 0; i < 4; i++)
			{
				int direction = diagionalDirections[i];
				int temporarySquare = square + direction;
				while (board.board[temporarySquare] == Global.empty)
					temporarySquare += direction;
				if ((board.board[temporarySquare] & (Global.diagionalBit + Global.border)) == (side == Global.white ? Global.whiteBishop : Global.blackBishop))
					return true;
			}

			return false;
		}
	}
}
