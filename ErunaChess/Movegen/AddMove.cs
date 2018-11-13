using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public static class AddMove
	{
		public static void CaptureMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = 0;
			moveList.count++;
		}
		public static void PawnCaptureMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = 0;
			moveList.count++;
		}
		public static void QuietMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = 0;
			moveList.count++;
		}
		public static void QuietPawnMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = 0;
			moveList.count++;
		}
		public static void PromotionMove(Board board, int move, MoveList moveList)
		{
			//add promotions
			moveList.moves[moveList.count].move = move + ((board.side == Global.white ? Global.whiteQueen : Global.blackQueen) << 22);
			moveList.moves[moveList.count].score = 0;	 
			moveList.count++;							 
			moveList.moves[moveList.count].move = move + ((board.side == Global.white ? Global.whiteBishop : Global.blackBishop) << 22);
			moveList.moves[moveList.count].score = 0;	 
			moveList.count++;							 
			moveList.moves[moveList.count].move = move + ((board.side == Global.white ? Global.whiteRook : Global.blackRook) << 22);
			moveList.moves[moveList.count].score = 0;	 
			moveList.count++;							 
			moveList.moves[moveList.count].move = move + ((board.side == Global.white ? Global.whiteKnight : Global.blackKnight) << 22);
			moveList.moves[moveList.count].score = 0;
			moveList.count++;
		}
		public static void EnpassantMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = move;
			moveList.count++;
		}
	}
}
