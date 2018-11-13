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
			MoveList moveList = new MoveList();
			IO.ParseFen(board, "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
			//IO.ParseFen(board, "rnbqkb1r/pp1p1pPp/8/2p1pP2/1P1P4/3P3P/P1P1P3/RNBQKBNR w KQkq e6 0 1");
			Board.Draw(board);
			Console.WriteLine(board.castlePermission);
			Console.WriteLine(board.enpassantSquare);
			Debug.DrawAttackedSquares(board, Global.white);
			Console.WriteLine();
			Debug.DrawAttackedSquares(board, Global.black);
			MoveGenerator.GenerateAllMoves(board, moveList);
			Console.ReadKey();
		}	
	}
}
