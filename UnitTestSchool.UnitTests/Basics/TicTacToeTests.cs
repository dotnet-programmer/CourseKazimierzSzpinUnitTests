namespace UnitTestSchool.UnitTests.Basics;

internal class TicTacToeTests
{
	private TicTacToe _ticTacToe;

	[SetUp]
	public void SetUp()
		=> _ticTacToe = new();

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInRow1_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ player, player, player },
			{ 0, 1, 0 },
			{ 2, 0, 1 },
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInRow2_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ 0, 1, 0 },
			{ player, player, player },
			{ 2, 0, 1 },
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInRow3_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ 0, 1, 0 },
			{ 2, 0, 1 },
			{ player, player, player },
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInColumn1_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ player, 0, 0 },
			{ player, 2, 1 },
			{ player, 0, 0 }
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInColumn2_ShouldReturnPlayerNumber(int player)
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
	public void SolveGame_WhenPlayerWinsInColumn3_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ 0, 0, player },
			{ 2, 1, player },
			{ 0, 0, player }
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInDiagonal_1_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ player, 0, 1 },
			{ 0, player, 0 },
			{ 2, 0, player }
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

	[TestCase(1)]
	[TestCase(2)]
	public void SolveGame_WhenPlayerWinsInDiagonal_2_ShouldReturnPlayerNumber(int player)
	{
		int[,] board = {
			{ 1, 0, player },
			{ 0, player, 0 },
			{ player, 0, 2 }
		};
		int result = _ticTacToe.SolveGame(board);
		result.Should().Be(player);
	}

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
}