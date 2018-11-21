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
			MovesList movesList = new MovesList();
			IO.ParseFen(board, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
			//IO.ParseFen(board, "rnbqkb1r/pp1p1pPp/8/2p1pP2/1P1P4/3P3P/P1P1P3/RNBQKBNR w KQkq e6 0 1");
			
			Perft.PerftTest(3, board);

			Console.ReadKey();
		}	
	}
}
