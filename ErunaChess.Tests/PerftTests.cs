using ErunaChess;

namespace ErunaChess.Tests
{
    public class Fixture
    {
        public Fixture() 
        {
            Init.All();
        }
    }

    public class PerftTests : IClassFixture<Fixture>
    {
        // https://www.chessprogramming.org/Perft_Results
        [Theory]
        [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5, 4865609 )] //startpos
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - ", 4, 4085603)] //pos 2
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - -", 5, 674624)] //pos 3
        [InlineData("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 4, 422333)] //pos 4
        [InlineData("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 4, 422333)] //pos 4 mirrored
        [InlineData("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 3, 62379)] //pos 5
        [InlineData("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 4, 3894594)] //pos 6
        public void Test1(string Fen, int depth, long nodes)
        {

            Init.All();
            Board board = new Board();
            IO.ParseFen(board, Fen);

            Assert.Equal(nodes,Perft.PerftTest(depth, board));
        }
    }
}