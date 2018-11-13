using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	static class IO
	{
		static private void AddPiece(Board board, int piece, int square)
		{
			board.board[square] = piece;
			board.pieces[piece].Add(square);
		}
		static public void ParseFen(Board board, string FEN )
		{
			//TODO : prevent bad FENs crashing the program

			//start at the upper rank
			Board.Reset(board);

			int i = 0;
			int sq = (int)Global.Square.A8;


			while (sq >= (int)Global.Square.A1) // Rank >= 1
			{
				switch (FEN[i])
				{
					case 'P': AddPiece(board, Global.whitePawn, sq); i++; sq++; continue;
					case 'N': AddPiece(board, Global.whiteKnight, sq); i++; sq++; continue;
					case 'B': AddPiece(board, Global.whiteBishop, sq); i++; sq++; continue;
					case 'R': AddPiece(board, Global.whiteRook, sq); i++; sq++; continue;
					case 'Q': AddPiece(board, Global.whiteQueen, sq); i++; sq++; continue;
					case 'K': AddPiece(board, Global.whiteKing, sq); i++; sq++; continue;
					case 'p': AddPiece(board, Global.blackPawn, sq); i++; sq++; continue;
					case 'n': AddPiece(board, Global.blackKnight, sq); i++; sq++; continue;
					case 'b': AddPiece(board, Global.blackBishop, sq); i++; sq++; continue;
					case 'r': AddPiece(board, Global.blackRook, sq); i++; sq++; continue;
					case 'q': AddPiece(board, Global.blackQueen, sq); i++; sq++; continue;
					case 'k': AddPiece(board, Global.blackKing, sq); i++; sq++; continue;

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
