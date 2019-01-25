using System;

namespace ErunaChess
{
	class Program
	{
		static void Main(string[] args)
		{
			Init.All();
			Board board = new Board();
			MovesList movesList = new MovesList();
			IO.ParseFen(board, "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10 "); 

			Perft.PerftTest(5, board);

			Console.ReadKey();
		}	
	}
}
