using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErunaChess
{
	public class MovesList
	{
		public List<Move> moves = new List<Move>();

		public Move this[int i]
		{
			get
			{
				return moves[i];
			}
			set
			{
				moves[i] = value;
			}
		}

		public int Count
		{
			get
			{
				return moves.Count;
			}
		}

		public void Add(Move move)
		{
			moves.Add(move);
		}
	}
}
