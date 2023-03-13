namespace ErunaChess
{
	public static class Init
	{
		public static void All()
		{
			Hash.Init();
			MakeMove.InitCastleBoard();
			MoveGenerator.InitMoveGenerator();
		}
	}
}
