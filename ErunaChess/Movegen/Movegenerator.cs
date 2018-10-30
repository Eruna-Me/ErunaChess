using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public static class MoveGenerator
	{
		public static void GenerateAllMoves(Board board, MoveList moveList)
		{
			// generate white pawn moves
			if (board.side == Global.white)
			{
				for (int i = 0; i < board.pieceCount[(int)Board.Pieces.whitePawn]; i++)
				{
					int square = board.pieces[(int)Board.Pieces.whitePawn, i];

					if(board.board[square + Global.boardWidth] == Global.empty)
					{
						AddMove.QuietPawnMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, false, false), moveList);
						//if on second rank
						if (square <= (int)Global.Square.H2 && board.board[square + Global.boardWidth * 2] == Global.empty)
							AddMove.QuietPawnMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, true, false), moveList);
						//if on 7th rank
						if (square >= (int)Global.Square.A7)
							AddMove.PromotionMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, false, false), moveList);
					}
					//captures
					if ((board.board[square + Global.boardWidth+1] & Global.border) == Global.blackBit)
						AddMove.CaptureMove(board, Move.Write(square, board.board[square + Global.boardWidth + 1], board.board[square + Global.boardWidth + 1], 0, false, false, false), moveList);

					if ((board.board[square + Global.boardWidth-1] & Global.border) == Global.blackBit)
						AddMove.CaptureMove(board, Move.Write(square, board.board[square + Global.boardWidth - 1], board.board[square + Global.boardWidth - 1], 0, false, false, false), moveList);
					
					//enpassant
					if (board.board[square + Global.boardWidth+1] == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.board[square + Global.boardWidth + 1], board.board[square + Global.boardWidth + 1], 0, true, false, false), moveList);

					if (board.board[square + Global.boardWidth-1] == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.board[square + Global.boardWidth - 1], board.board[square + Global.boardWidth - 1], 0, true, false, false), moveList);
				}
			}
			// generate black pawn moves
			if (board.side == Global.black)
			{
				for (int i = 0; i < board.pieceCount[(int)Board.Pieces.blackPawn]; i++)
				{
					int square = board.pieces[(int)Board.Pieces.blackPawn, i];

					if (board.board[square + Global.boardWidth] == Global.empty)
					{
						AddMove.QuietPawnMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, false, false), moveList);
						//if on 7th rank
						if (square >= (int)Global.Square.A7 && board.board[square + Global.boardWidth * 2] == Global.empty)
							AddMove.QuietPawnMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, true, false), moveList);
						//if on 2nd rank
						if (square <= (int)Global.Square.H2)
							AddMove.PromotionMove(board, Move.Write(square, square + Global.boardWidth, 0, 0, false, false, false), moveList);
					}

					//captures
					if ((board.board[square - Global.boardWidth+1] & Global.border) == Global.whiteBit)
						AddMove.CaptureMove(board, Move.Write(square, board.board[square - Global.boardWidth + 1], board.board[square - Global.boardWidth + 1], 0, false, false, false), moveList);

					if ((board.board[square - Global.boardWidth-1] & Global.border) == Global.whiteBit)
						AddMove.CaptureMove(board, Move.Write(square, board.board[square - Global.boardWidth - 1], board.board[square - Global.boardWidth - 1], 0, false, false, false), moveList);

					//enpassant
					if (board.board[square - Global.boardWidth + 1] == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.board[square - Global.boardWidth + 1], board.board[square - Global.boardWidth + 1], 0, true, false, false), moveList);

					if (board.board[square - Global.boardWidth - 1] == board.enpassantSquare)
						AddMove.EnpassantMove(board, Move.Write(square, board.board[square - Global.boardWidth - 1], board.board[square - Global.boardWidth- 1], 0, true, false, false), moveList);
				}
			}
		}
	}
}
