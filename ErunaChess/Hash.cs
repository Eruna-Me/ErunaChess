using System;
using System.Collections.Generic;
using static ErunaChess.Global.Square;
using static ErunaChess.Global;

namespace ErunaChess
{
	class Hash
	{
		const int enPassant = 0;
		public static long sideKey;
		public static long[] castleKeys = new long[16];
		static readonly Random random = new Random();

		public static Dictionary<int, long[]> pieceKeys = new Dictionary<int, long[]>()
		{
			{ enPassant,			new long[boardSize] },
			{ blackPawn,		new long[boardSize] },
			{ whitePawn,		new long[boardSize] },
			{ blackKnight,	new long[boardSize] },
			{ whiteKnight,	new long[boardSize] },
			{ blackBishop,	new long[boardSize] },
			{ whiteBishop,	new long[boardSize] },
			{ blackRook,		new long[boardSize] },
			{ whiteRook,		new long[boardSize] },
			{ blackQueen,	new long[boardSize] },
			{ whiteQueen,	new long[boardSize] },
			{ blackKing,		new long[boardSize] },
			{ whiteKing,		new long[boardSize] }
		};

		public static void Init()
		{
			foreach (KeyValuePair<int, long[]> i in pieceKeys)
			{
				for (int j = 0; j < boardSize; ++j)
				{
					pieceKeys[i.Key][j] = NextLong(random);
				}
			}
			sideKey = NextLong(random);
			for (int i = 0; i < 16; ++ i)
			{
				castleKeys[i] = NextLong(random);
			}
		}

		public static long NextLong(Random random)
		{
			byte[] buffer = new byte[8];
			random.NextBytes(buffer);
			return BitConverter.ToInt64(buffer, 0);
		}

		public static long GenerateKey(Board board)
		{
			//TODO redo this all in a faster way, although this piece of code probably doesn't need to be very fast.

			int piece, sq;
			long key = 0;

			for(sq = 0; sq < boardSize; sq++)
			{
				piece = board[sq];
				if (piece < border && piece > empty)
				{
					key ^= pieceKeys[piece][sq];
				}
			}

			if(board.side == white)	key ^= sideKey;

			if (board.enpassantSquare != (int)offBoard) key ^= pieceKeys[enPassant][board.enpassantSquare];

			key ^= castleKeys[board.castlePermission];

			return 0;
		}
	}
}
