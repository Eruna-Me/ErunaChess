using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	class Global
	{
		public const int white = whiteBit;
		public const int black = blackBit;

		public const int boardWidth = 16;
		public const int boardHeight = 16;
		public const int boardSize = boardWidth * boardHeight;//Let's use this for now, the slightly less space unfriendly 15 * 12 and 16*12 are also very tempting
		public enum Square {
			A1 = 68, B1, C1, D1, E1, F1, G1, H1,
			A2 = 84, B2, C2, D2, E2, F2, G2, H2,
			A3 = 100, B3, C3, D3, E3, F3, G3, H3,
			A4 = 116, B4, C4, D4, E4, F4, G4, H4,
			A5 = 132, B5, C5, D5, E5, F5, G5, H5,
			A6 = 148, B6, C6, D6, E6, F6, G6, H6,
			A7 = 164, B7, C7, D7, E7, F7, G7, H7,
			A8 = 180, B8, C8, D8, E8, F8, G8, H8,
			offBoard
		}

		public const int empty = 0;
		public const int border = 48;

		public const int kingBits = 3;
		public const int orthogonalBit = 4;
		public const int diagionalBit = 8;
		public const int whiteBit = 16;
		public const int blackBit = 32;

		//white piece constants			4 = orthogonal 8 = diagional 16 = white 32 = black		pawn =1 (no2) knight = 2(no 1) king = 1 and 2 (can this be done in a smarter way?)
		public const int whitePawn = 17;
		public const int whiteKnight = 18;
		public const int whiteBishop =	whiteBit + diagionalBit;
		public const int whiteRook =	whiteBit + orthogonalBit;
		public const int whiteQueen =	whiteBit + orthogonalBit + diagionalBit;
		public const int whiteKing =	whiteBit + kingBits;
		//black piece constants
		public const int blackPawn = 33;
		public const int blackKnight = 34;
		public const int blackBishop =	blackBit + diagionalBit;
		public const int blackRook =	blackBit + orthogonalBit;
		public const int blackQueen =	blackBit + orthogonalBit + diagionalBit;
		public const int blackKing =	blackBit + kingBits;

		public const int whiteKingSideCastle = 1;
		public const int whiteQueenSideCastle = 2;
		public const int blackKingSideCastle = 4;
		public const int blackQueenSideCastle = 8;
	}
}
// E.g. 16-31 for white pieces, 32-47 for black pieces, 48 for guards. With board[toSqr]&stm (stm = 16 or 32)
//	1kp 2kn 4s 8d		16 32 64 128