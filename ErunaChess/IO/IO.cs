using System;
using static ErunaChess.Global;
using static ErunaChess.Global.Square;

namespace ErunaChess
{
	public static class IO
	{
		static private void AddPiece(Board board, int piece, int square)
		{
			board[square] = piece;
			board.pieces[piece].Add(square);
		}
		static public void ParseFen(Board board, string FEN )
		{
			//TODO : prevent bad FENs crashing the program

			//start at the upper rank
			Board.Reset(board);

			int i = 0;
			int sq = (int)A8;


			while (sq >= (int)A1) // Rank >= 1
			{
				switch (FEN[i])
				{
					case 'P': AddPiece(board, whitePawn, sq); i++; sq++; continue;
					case 'N': AddPiece(board, whiteKnight, sq); i++; sq++; continue;
					case 'B': AddPiece(board, whiteBishop, sq); i++; sq++; continue;
					case 'R': AddPiece(board, whiteRook, sq); i++; sq++; continue;
					case 'Q': AddPiece(board, whiteQueen, sq); i++; sq++; continue;
					case 'K': AddPiece(board, whiteKing, sq); i++; sq++; continue;
					case 'p': AddPiece(board, blackPawn, sq); i++; sq++; continue;
					case 'n': AddPiece(board, blackKnight, sq); i++; sq++; continue;
					case 'b': AddPiece(board, blackBishop, sq); i++; sq++; continue;
					case 'r': AddPiece(board, blackRook, sq); i++; sq++; continue;
					case 'q': AddPiece(board, blackQueen, sq); i++; sq++; continue;
					case 'k': AddPiece(board, blackKing, sq); i++; sq++; continue;

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
						sq = sq - boardWidth - 8;  //Go one rank down
						continue;

					default: Console.WriteLine("invalid char D:"); break;
				}
			}

			board.side = FEN[i] == 'w' ? white : black;

			i += 2;

			while(FEN[i] != ' ')
			{
				switch(FEN[i])
				{
					case 'K': board.castlePermission |= whiteKingSideCastle;		break;
					case 'Q': board.castlePermission |= whiteQueenSideCastle;	break;
					case 'k': board.castlePermission |= blackKingSideCastle;		break;
					case 'q': board.castlePermission |= blackQueenSideCastle;	break;
					default: break;
				}
				i++;
			}

			i++;

			if (FEN[i] != '-')
			{
				int file = FEN[i] - 'a';
				int rank = FEN[i+1] - '1';

				board.enpassantSquare =(rank * 16) + file + (int)A1;
			}

			//fifty moves

			//Fullmoves (do I even need to know this?)
		}
	}
}
