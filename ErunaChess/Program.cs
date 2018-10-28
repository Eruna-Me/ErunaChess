using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	class Program
	{
		static void Main(string[] args)
		{
			Init.All();
			Board board = new Board();
			IO.ParseFen(board, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
			Board.Draw(board);
			Console.WriteLine(board.castlePermission);
			Console.WriteLine(board.enpassantSquare);
			Debug.DrawAttackedSquares(board, Global.white);
			Console.WriteLine();
			Debug.DrawAttackedSquares(board, Global.black);
			Console.ReadKey();
		}
	
	}
}
