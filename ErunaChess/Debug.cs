using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	class Debug
	{
		public static void DrawAttackedSquares(Board board, int side )
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (Attack.SquareAttacked(board, (i * 16) + j + (int)Global.Square.A1, side))
						Console.Write('X');
					else
						Console.Write('-');
				}
				Console.WriteLine();
			}
		}

		public static void WriteDownAllMoves(MovesList movesList)
		{
			Console.WriteLine(movesList.moves.Count + " moves");

			for (int i = 0; i < movesList.moves.Count; i++)
			{
				Console.WriteLine();
				Console.WriteLine("From: " + Move.From(movesList.moves[i].move));
				Console.WriteLine("to:   " + Move.To(movesList.moves[i].move));
			}
		}
	}
}
