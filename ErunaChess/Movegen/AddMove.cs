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
			moveList.moves[moveList.count].score = move;
			moveList.count++;
		}
		public static void QuietMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = move;
			moveList.count++;
		}
		public static void QuietPawnMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = move;
			moveList.count++;
		}
		public static void PawnCaptureMove(Board board, int move, MoveList moveList)
		{
			moveList.moves[moveList.count].move = move;
			moveList.moves[moveList.count].score = move;
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
