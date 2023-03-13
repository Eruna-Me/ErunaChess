using System;
using System.Diagnostics;

namespace ErunaChess
{
	static public class Perft
    {
		static long leafNodes;

		static void _Perft(int depth, Board board)
		{
			if (depth == 0)
			{
				leafNodes++;
				return;
			}

			MovesList movesList = new MovesList();
			MoveGenerator.GenerateAllMoves(board, movesList);

			int MoveNum = 0;
			for (MoveNum = 0; MoveNum < movesList.Count; ++MoveNum)
			{

				if (!MakeMove.Make(board, movesList[MoveNum].move))
				{
					continue;
				}
				_Perft(depth - 1, board);
				MakeMove.Take(board);
			}

			return;
		}

		static public long PerftTest(int depth, Board board)
		{
			Stopwatch stopwatch = new Stopwatch();
			Board.Draw(board);
			leafNodes = 0;
			stopwatch.Start();
			MovesList movesList = new MovesList();
			MoveGenerator.GenerateAllMoves(board, movesList);

			int move;
			int MoveNum = 0;
			for (MoveNum = 0; MoveNum < movesList.Count; ++MoveNum)
			{
				move = movesList[MoveNum].move;
				if (!MakeMove.Make(board, move))
				{
					continue;
				}
				long cumnodes = leafNodes;
				_Perft(depth - 1, board);
				MakeMove.Take(board);
				long oldnodes = leafNodes - cumnodes;
				Console.Write($"move {MoveNum + 1} : { move} : {oldnodes}\n" );
			}

			Console.Write($"\nTest Complete : {leafNodes} nodes visited in {stopwatch.ElapsedMilliseconds}ms, speed = {leafNodes/stopwatch.ElapsedMilliseconds}kn/s\n" );

			return leafNodes;
		}
	}
}
