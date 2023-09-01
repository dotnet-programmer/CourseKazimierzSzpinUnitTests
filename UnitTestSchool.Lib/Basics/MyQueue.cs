namespace UnitTestSchool.Lib.Basics;

public class MyQueue<T>
{
	private readonly List<T> _list = new();

	public int Count => _list.Count;

	public void Enqueue(T value) => _list.Add(value);

	public T Peek()
	{
		if (_list.Count == 0)
		{
			throw new InvalidOperationException();
		}

		return _list[0];
	}

	public T Dequeue()
	{
		if (_list.Count == 0)
		{
			throw new InvalidOperationException();
		}

		var result = _list[0];
		_list.RemoveAt(0);

		return result;
	}
}