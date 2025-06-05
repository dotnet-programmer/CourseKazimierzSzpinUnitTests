namespace UnitTestSchool.Lib.Basics;

public class MyQueue<T>
{
	private readonly List<T> _list = [];

	public int Count
		=> _list.Count;

	// dodaj element na koniec kolejki
	public void Enqueue(T value)
	{
		ArgumentNullException.ThrowIfNull(value, nameof(value));
		_list.Add(value);
	}

	// usuwa pierwszy element i zwraca go
	public T Dequeue()
	{
		if (_list.Count == 0)
		{
			throw new InvalidOperationException();
		}

		T result = _list[0];
		_list.RemoveAt(0);
		return result;
	}

	// zwraca pierwszy element z kolejki, ale nie usuwa tego elementu
	public T Peek()
		=> _list.Count == 0 ? throw new InvalidOperationException() : _list[0];
}