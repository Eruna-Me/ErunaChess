using System;
using System.Collections.Generic;
using static ErunaChess.Global;
using static ErunaChess.Global.Square;

namespace ErunaChess
{
	public class Board
	{
		public int[] board = new int[boardSize];
		public List<int>[] pieces = new List<int>[border];

		public int this[int i]
		{
			get => board[i];
			set => board[i] = value;
		}

		public int this[Square sq]
		{
			get => board[(int)sq];
			set => board[(int)sq] = value;
		}

		public int side;
		public int enpassantSquare;
		public int castlePermission;

		public int fiftyMove;
		public int ply;
		public int historyPly;

		public ulong hashKey;

		public History[] history = new History[2048];//TODO turn this into a list or something

		public Board ()
		{
			for(int i = 0; i < 2048; i++)
			{
				history[i] = new History();
			}

			pieces[blackPawn] = new List<int>();
			pieces[whitePawn] = new List<int>();
			pieces[blackKnight] = new List<int>();
			pieces[whiteKnight] = new List<int>();
			pieces[blackBishop] = new List<int>();
			pieces[whiteBishop] = new List<int>();
			pieces[whiteRook] = new List<int>();
			pieces[blackRook] = new List<int>();
			pieces[blackQueen] = new List<int>();
			pieces[whiteQueen] = new List<int>();
			pieces[blackKing] = new List<int>();
			pieces[whiteKing] = new List<int>();
		}

		public static void Draw(Board board)
		{
			for(int i = 0; i < boardSize; i++)
			{
				Console.Write("{0,4}", board[i]);
				if ((i+1) % 16 == 0) Console.WriteLine();
			}
		}

		public static void Reset(Board board)
		{
			for (int i = 0; i < boardSize; i++)
			{
				board[i] = border;
			}

			for (int i = 0; i < 8; i++)
			{
				for(int j = 0; j < 8; j++)
				{
					board[(i*16) + j + (int)A1] = empty;
				}
			}

			board.side = 2;
			board.enpassantSquare = (int)offBoard;
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
		public int castlePermission;
		public int fiftymove;

		public ulong hashKey;
	}
}
