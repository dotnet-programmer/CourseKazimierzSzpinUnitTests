﻿namespace UnitTestSchool.Lib.Basics;

public class MyStack<T>
{
	private readonly List<T> _list = [];

	public int Count
		=> _list.Count;

	public void Push(T obj)
	{
		ArgumentNullException.ThrowIfNull(obj, nameof(obj));
		_list.Add(obj);
	}

	public T Pop()
	{
		if (_list.Count == 0)
		{
			throw new InvalidOperationException();
		}

		var result = _list[_list.Count - 1];
		_list.RemoveAt(_list.Count - 1);
		return result;
	}

	public T Peek()
		=> _list.Count == 0 ? throw new InvalidOperationException() : _list[_list.Count - 1];
}