using System.Collections.Generic;

namespace ErunaChess
{
	public class MovesList
	{
		public List<Move> moves = new List<Move>();

		public Move this[int i]
		{
			get => moves[i];
			set => moves[i] = value;
		}

		public int Count => moves.Count;

		public void Add(Move move) => moves.Add(move);
	}
}
