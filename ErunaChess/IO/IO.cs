using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	static class IO
	{
		static public void ParseFen(Board board, string FEN )
		{
			//prevent bad FENs crashing the program

			//start at the upper rank
			Board.Reset(board);

			int i = 0;
			int sq = (int)Global.Square.A8;


			while (sq >= (int)Global.Square.A1) // Rank >= 1
			{
				switch (FEN[i])
				{
					case 'P': board.board[sq] = Global.whitePawn;	board.pieces[(int)Board.Pieces.whitePawn, board.pieceCount[(int)Board.Pieces.whitePawn]] = sq; board.pieceCount[(int)Board.Pieces.whitePawn]++;	i++; sq++; continue;
					case 'N': board.board[sq] = Global.whiteKnight; board.pieces[(int)Board.Pieces.whiteKnight, board.pieceCount[(int)Board.Pieces.whiteKnight]] = sq; board.pieceCount[(int)Board.Pieces.whiteKnight]++;	i++; sq++; continue;
					case 'B': board.board[sq] = Global.whiteBishop; board.pieces[(int)Board.Pieces.whiteBishop, board.pieceCount[(int)Board.Pieces.whiteBishop]] = sq; board.pieceCount[(int)Board.Pieces.whiteBishop]++;	i++; sq++; continue;
					case 'R': board.board[sq] = Global.whiteRook;	board.pieces[(int)Board.Pieces.whiteRook, board.pieceCount[(int)Board.Pieces.whiteRook]] = sq; board.pieceCount[(int)Board.Pieces.whiteRook]++;	i++; sq++; continue;
					case 'Q': board.board[sq] = Global.whiteQueen;	board.pieces[(int)Board.Pieces.whiteQueen, board.pieceCount[(int)Board.Pieces.whiteQueen]] = sq; board.pieceCount[(int)Board.Pieces.whiteQueen]++;	i++; sq++; continue;
					case 'K': board.board[sq] = Global.whiteKing;	board.pieces[(int)Board.Pieces.whiteKing, board.pieceCount[(int)Board.Pieces.whiteKing]] = sq; board.pieceCount[(int)Board.Pieces.whiteKing]++;	i++; sq++; continue;
					case 'p': board.board[sq] = Global.blackPawn;	board.pieces[(int)Board.Pieces.blackPawn, board.pieceCount[(int)Board.Pieces.blackPawn]] = sq; board.pieceCount[(int)Board.Pieces.blackPawn]++;	i++; sq++; continue;
					case 'n': board.board[sq] = Global.blackKnight; board.pieces[(int)Board.Pieces.blackKnight, board.pieceCount[(int)Board.Pieces.blackKnight]] = sq; board.pieceCount[(int)Board.Pieces.blackKnight]++;	i++; sq++; continue;
					case 'b': board.board[sq] = Global.blackBishop; board.pieces[(int)Board.Pieces.blackBishop, board.pieceCount[(int)Board.Pieces.blackBishop]] = sq; board.pieceCount[(int)Board.Pieces.blackBishop]++;	i++; sq++; continue;
					case 'r': board.board[sq] = Global.blackRook;	board.pieces[(int)Board.Pieces.blackRook, board.pieceCount[(int)Board.Pieces.blackRook]] = sq; board.pieceCount[(int)Board.Pieces.blackRook]++;	i++; sq++; continue;
					case 'q': board.board[sq] = Global.blackQueen;	board.pieces[(int)Board.Pieces.blackQueen, board.pieceCount[(int)Board.Pieces.blackQueen]] = sq; board.pieceCount[(int)Board.Pieces.blackQueen]++;	i++; sq++; continue;
					case 'k': board.board[sq] = Global.blackKing;	board.pieces[(int)Board.Pieces.blackKing, board.pieceCount[(int)Board.Pieces.blackKing]]  = sq; board.pieceCount[(int)Board.Pieces.blackKing]++;	i++; sq++; continue;

					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
						sq += FEN[i] - '0';
						i++;
						continue;
						
					case '/':
					case ' ':
						i++;
						sq = sq - Global.boardWidth - 8;  //Go one rank down
						continue;

					default: Console.WriteLine("invalid char D:"); break;
				}
			}

			board.side = FEN[i] == 'w' ? Global.white : Global.black;

			i += 2;

			while(FEN[i] != ' ')
			{
				switch(FEN[i])
				{
					case 'K': board.castlePermission |= Global.whiteKingSideCastle;		break;
					case 'Q': board.castlePermission |= Global.whiteQueenSideCastle;	break;
					case 'k': board.castlePermission |= Global.blackKingSideCastle;		break;
					case 'q': board.castlePermission |= Global.blackQueenSideCastle;	break;
					default: break;
				}
				i++;
			}

			i++;

			if (FEN[i] != '-')
			{
				int file = FEN[i] - 'a';
				int rank = FEN[i+1] - '1';

				board.enpassantSquare =(rank * 16) + file + (int)Global.Square.A1;
			}

			//fifty moves

			//Fullmoves (do I even need to know this?)
		}
	}
}
