using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public class Board
	{
		public int[] board = new int[Global.boardSize];
		public int[,] pieces = new int[12, 10]; //I need to think of a smarter way of doing this.

		public int side;
		public int enpassantSquare;
		public byte castlePermission; // it might be better to do this with an int

		public int fiftyMove;
		public int ply;
		public int historyPly;

		public ulong hashKey;

		public int[] pieceCount = new int[12];

		public Undo[] history = new Undo[2048];//the played moves should never hit 2048, still this looks like a bad solution?

		public static void Draw(Board board)
		{
			for(int i = 0; i < Global.boardSize; i++)
			{
				Console.Write("{0,4}", board.board[i]);
				if ((i+1) % 16 == 0) Console.WriteLine();
			}
		}

		public static void Reset(Board board)
		{
			for (int i = 0; i < Global.boardSize; i++)
			{
				board.board[i] = Global.border;
			}

			for (int i = 0; i < 8; i++)
			{
				for(int j = 0; j < 8; j++)
				{
					board.board[(i*16) + j + (int)Global.Square.A1] = Global.empty;
				}
			}

			board.side = 2;
			board.enpassantSquare = (int)Global.Square.offBoard;
			board.fiftyMove = 0;
			board.ply = 0;
			board.historyPly = 0;
			board.castlePermission = 0;
			board.hashKey = 0;
		}
	}

	public class Undo // TODO think of a sensible name, and move this
	{
		public int move;
		public int enpas;
		public int castleperm;
		public int fiftymove;

		public ulong hashKey;
	}
}
