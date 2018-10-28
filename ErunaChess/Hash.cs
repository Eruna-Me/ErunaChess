using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	class Hash
	{
		public static long sideKey;
		public static long[] castleKeys = new long[16];
		public static long[,] pieceKeys = new long[13, Global.boardSize];// this is probably a slight waste of memory
		static readonly Random random = new Random();

		public static void Init()
		{
			for (int i = 0; i < 12; ++i)
			{
				for (int j = 0; j < Global.boardSize; ++j)
				{
					pieceKeys[i, j] = NextLong(random);
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
				if (piece <= 12) // I need to think of some smart bit stuff for the pieces.
				{
					key ^= pieceKeys[piece,sq];
				}
			}

			if(board.side == Global.white)	key ^= sideKey;

			if (board.enpassantSquare != (int)Global.Square.offBoard) key ^= pieceKeys[0,board.enpassantSquare];

			key ^= castleKeys[board.castlePermission];

			return 0;
		}
	}
}
