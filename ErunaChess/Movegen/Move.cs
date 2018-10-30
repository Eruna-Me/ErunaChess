using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public class Move
	{
		public int move;
		public int score;

		public static int From(int square) =>  square & 0xFF;
		public static int To(int square) => (square >> 8) & 0xFF;
		public static int Captured(int piece) => (piece >> 16) & 0x1F;
		public static int Promoted(int piece) => (piece >> 22) & 0x1F;

		public static int EnPassantFlag() => 1 >> 28;
		public static int PawnStartFlag() => 1 >> 29;
		public static int CastleFlag() => 1 >> 30;

		public static int CaptureFlag() => 0x1F >> 16;
		public static int PromotedFlag() => 0x1F >> 22;

		static public int Write(int from, int to, int captured, int promoted, bool enPassantFlag, bool pawnStartFlag, bool castleFlag)
		{
			int move = 0;

			move += From(from);
			move += To(to);
			move += Captured(captured);
			move += Promoted(promoted);

			if (enPassantFlag)	move += EnPassantFlag();
			if (pawnStartFlag)	move += PawnStartFlag();
			if (castleFlag)		move += CastleFlag();

			return move;
		}
	}
}
