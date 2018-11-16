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
		public Dictionary<int, List<int>> pieces = new Dictionary<int, List<int>>()
		{
			{ Global.blackPawn, new List<int>() },
			{ Global.whitePawn, new List<int>() },
			{ Global.blackKnight, new List<int>() },
			{ Global.whiteKnight, new List<int>() },
			{ Global.blackBishop, new List<int>() },
			{ Global.whiteBishop, new List<int>() },
			{ Global.blackRook, new List<int>() },
			{ Global.whiteRook, new List<int>() },
			{ Global.blackQueen, new List<int>() },
			{ Global.whiteQueen, new List<int>() },
			{ Global.blackKing, new List<int>() },
			{ Global.whiteKing, new List<int>() }
		};

		public int side;
		public int enpassantSquare;
		public int castlePermission;

		public int fiftyMove;
		public int ply;
		public int historyPly;

		public ulong hashKey;

		public History[] history = new History[2048];//TODO turn this into a list or something

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

	public class History
	{
		public int move;
		public int enpassantSquare;
		public int castleperm;
		public int fiftymove;

		public ulong hashKey;
	}
}
