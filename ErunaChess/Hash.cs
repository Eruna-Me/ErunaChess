using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			{ enPassant,			new long[Global.boardSize] },
			{ Global.blackPawn,		new long[Global.boardSize] },
			{ Global.whitePawn,		new long[Global.boardSize] },
			{ Global.blackKnight,	new long[Global.boardSize] },
			{ Global.whiteKnight,	new long[Global.boardSize] },
			{ Global.blackBishop,	new long[Global.boardSize] },
			{ Global.whiteBishop,	new long[Global.boardSize] },
			{ Global.blackRook,		new long[Global.boardSize] },
			{ Global.whiteRook,		new long[Global.boardSize] },
			{ Global.blackQueen,	new long[Global.boardSize] },
			{ Global.whiteQueen,	new long[Global.boardSize] },
			{ Global.blackKing,		new long[Global.boardSize] },
			{ Global.whiteKing,		new long[Global.boardSize] }
		};

		public static void Init()
		{
			foreach (KeyValuePair<int, long[]> i in pieceKeys)
			{
				for (int j = 0; j < Global.boardSize; ++j)
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

			for(sq = 0; sq < Global.boardSize; sq++)
			{
				piece = board.board[sq];
				if (piece < Global.border && piece > Global.empty)
				{
					key ^= pieceKeys[piece][sq];
				}
			}

			if(board.side == Global.white)	key ^= sideKey;

			if (board.enpassantSquare != (int)Global.Square.offBoard) key ^= pieceKeys[enPassant][board.enpassantSquare];

			key ^= castleKeys[board.castlePermission];

			return 0;
		}
	}
}
