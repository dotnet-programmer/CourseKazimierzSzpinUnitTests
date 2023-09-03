namespace UnitTestSchool.UnitTests.Basics;

internal class TicTacToeTests
{
	private TicTacToe _ticTacToe;

	[SetUp]
	public void SetUp() => _ticTacToe = new();

	[Test]
	public void SolveGame_WhenTheGameIsNotOver_ShouldReturnMinusOne()
	{
		int[,] board = { 
			{ 0, 0, 1 }, 
			{ 0, 1, 0 }, 
			{ 2, 2, 0 } 
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(-1);
	}

	[Test]
	public void SolveGame_WhenTie_ShouldReturnZero()
	{
		int[,] board = { 
			{ 2, 1, 1 }, 
			{ 1, 1, 2 }, 
			{ 2, 2, 1 } 
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(0);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInRow_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = { 
			{ 0, 1, 0 }, 
			{ player, player, player }, 
			{ 2, 0, 1 } 
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInColumn_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = { 
			{ 0, player, 0 }, 
			{ 2, player, 1 }, 
			{ 0, player, 0 } 
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInDiagonal_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ 1, 0, player },
			{ 0, player, 0 },
			{ player, 0, 2 }
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}
}