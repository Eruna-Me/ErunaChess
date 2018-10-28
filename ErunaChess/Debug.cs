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
					if (Attack.SquareAttacked(side, (i * 16) + j + (int)Global.Square.A1, board))
						Console.Write('X');
					else
						Console.Write('-');
				}
				Console.WriteLine();
			}
		}
	}
}
