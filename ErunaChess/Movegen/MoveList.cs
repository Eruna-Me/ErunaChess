using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public class MoveList
	{
		public MoveList()
		{
			for(int i = 0; i < 256; i++)
			{
				moves[i] = new Move();
			}
		}
		public Move[] moves = new Move[256]; //it might make more sense to put this in a list or something.
		public int count;
	}
}
