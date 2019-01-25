namespace ErunaChess
{
	static class Init
	{
		public static void All()
		{
			Hash.Init();
			MakeMove.InitCastleBoard();
			MoveGenerator.InitMoveGenerator();
		}
	}
}
