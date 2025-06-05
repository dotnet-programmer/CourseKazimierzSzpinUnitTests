namespace UnitTestSchool.Lib.Basics;

public class TicTacToe
{
	private int _winner;

	// 3x3
	// 0 - puste
	// 1 - krzyżyk
	// 2 - kółko

	// INPUT:
	// [ [0,0,1]
	//   [0,1,2]
	//   [2,1,0] ]

	// OUTPUT:
	// zwraca -1 - rozgrywka nie jest zakończona
	// zwraca 0 - remis
	// zwraca 1 - wygrywa krzyżyk
	// zwraca 2 - wygrywa kółko
	public int SolveGame(int[,] board)
		=> RowIsWon(board) || ColumnIsWon(board) || DiagonalIsWon(board) ? _winner : GameIsInProgress(board);

	private bool DiagonalIsWon(int[,] board)
	{
		var result = false;
		var diagonal = GetDiagonal(board);

		for (var i = 0; i < 2; i++)
		{
			if (CheckIfGameIsWon(diagonal[i]))
			{
				_winner = diagonal[i][0];
				result = true;
				break;
			}
		}

		return result;
	}

	private static List<List<int>> GetDiagonal(int[,] board)
	{
		List<List<int>> diagonal = [[], []];

		for (var i = 0; i < board.GetLength(0); i++)
		{
			diagonal[0].Add(board[i, i]);
		}

		for (int i = board.GetLength(0) - 1, j = 0; i >= 0; i--, j++)
		{
			diagonal[1].Add(board[i, j]);
		}

		return diagonal;
	}

	private bool ColumnIsWon(int[,] board)
	{
		var result = false;

		for (var i = 0; i < board.GetLength(1); i++)
		{
			var column = GetColumn(board, i);

			if (CheckIfGameIsWon(column))
			{
				_winner = column[0];
				result = true;
				break;
			}
		}

		return result;
	}

	private static List<int> GetColumn(int[,] board, int j)
	{
		List<int> col = [];

		for (var i = 0; i < board.GetLength(0); i++)
		{
			col.Add(board[i, j]);
		}

		return col;
	}

	private bool RowIsWon(int[,] board)
	{
		var result = false;

		for (var i = 0; i < board.GetLength(1); i++)
		{
			var row = GetRow(board, i);

			if (CheckIfGameIsWon(row))
			{
				_winner = row[0];
				result = true;
				break;
			}
		}

		return result;
	}

	private static bool CheckIfGameIsWon(IReadOnlyCollection<int> row)
		=> row.All(x => x == row.First());

	private static List<int> GetRow(int[,] board, int i)
	{
		List<int> row = [];

		for (var j = 0; j < board.GetLength(1); j++)
		{
			row.Add(board[i, j]);
		}

		return row;
	}

	private static int GameIsInProgress(int[,] board)
		=> board.Cast<int>().Contains(0) ? -1 : 0;
}