using static ErunaChess.Global;

namespace ErunaChess
{
	public static class AddMove
	{
		public static void CaptureMove(Board board, int move, MovesList movesList)		=> movesList.Add(new Move(move, 0));

		public static void PawnCaptureMove(Board board, int move, MovesList movesList)	=> movesList.Add(new Move(move, 0));

		public static void QuietMove(Board board, int move, MovesList movesList)			=> movesList.Add(new Move(move, 0));

		public static void QuietPawnMove(Board board, int move, MovesList movesList)		=> movesList.Add(new Move(move, 0));

		public static void EnpassantMove(Board board, int move, MovesList movesList)		=> movesList.Add(new Move(move, 0));

		public static void PromotionMove(Board board, int move, MovesList movesList)
		{ 
			movesList.Add(new Move(move + ((board.side += queenBit) << 22), 0));
			movesList.Add(new Move(move + ((board.side += diagionalBit) << 22), 0));
			movesList.Add(new Move(move + ((board.side += orthogonalBit) << 22), 0));
			movesList.Add(new Move(move + ((board.side += knightBit) << 22), 0));
		}
	}
}
