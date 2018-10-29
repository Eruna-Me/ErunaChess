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
					case 'P': board.board[sq] = Global.whitePawn;	i++; sq++; continue;
					case 'N': board.board[sq] = Global.whiteKnight; i++; sq++; continue;
					case 'B': board.board[sq] = Global.whiteBishop; i++; sq++; continue;
					case 'R': board.board[sq] = Global.whiteRook;	i++; sq++; continue;
					case 'Q': board.board[sq] = Global.whiteQueen;	i++; sq++; continue;
					case 'K': board.board[sq] = Global.whiteKing;	i++; sq++; continue;
					case 'p': board.board[sq] = Global.blackPawn;	i++; sq++; continue;
					case 'n': board.board[sq] = Global.blackKnight; i++; sq++; continue;
					case 'b': board.board[sq] = Global.blackBishop; i++; sq++; continue;
					case 'r': board.board[sq] = Global.blackRook;	i++; sq++; continue;
					case 'q': board.board[sq] = Global.blackQueen;	i++; sq++; continue;
					case 'k': board.board[sq] = Global.blackKing;	i++; sq++; continue;

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
