using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public static class AddMove
	{
		public static void CaptureMove(Board board, int move, MovesList movesList)		=> movesList.moves.Add(new Move(move, 0));

		public static void PawnCaptureMove(Board board, int move, MovesList movesList)	=> movesList.moves.Add(new Move(move, 0));

		public static void QuietMove(Board board, int move, MovesList movesList)			=> movesList.moves.Add(new Move(move, 0));

		public static void QuietPawnMove(Board board, int move, MovesList movesList)		=> movesList.moves.Add(new Move(move, 0));

		public static void EnpassantMove(Board board, int move, MovesList movesList)		=> movesList.moves.Add(new Move(move, 0));

		public static void PromotionMove(Board board, int move, MovesList movesList)
		{
			//add promotions
			movesList.moves.Add(new Move(move + ((board.side == Global.white ? Global.whiteQueen : Global.blackQueen) << 22), 0));
			movesList.moves.Add(new Move(move + ((board.side == Global.white ? Global.whiteBishop : Global.blackBishop) << 22), 0));
			movesList.moves.Add(new Move(move + ((board.side == Global.white ? Global.whiteRook : Global.blackRook) << 22), 0));
			movesList.moves.Add(new Move(move + ((board.side == Global.white ? Global.whiteKnight : Global.blackKnight) << 22), 0));
		}
	}
}
