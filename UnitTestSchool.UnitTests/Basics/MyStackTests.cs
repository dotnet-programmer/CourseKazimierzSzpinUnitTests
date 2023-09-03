using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnitTestSchool.UnitTests.Basics;
internal class MyStackTests
{
	private MyStack<int?> _stack;

	[SetUp]
	public void SetUp()
	{
		_stack = new();
	}

	#region Push

	[Test]
	public void Push_WhenArgumentIsNull_ShouldThrowArgumentNullException()
	{
		Action action = () => _stack.Push(null);
		action.Should().ThrowExactly<ArgumentNullException>().WithMessage("*obj*");
	}

	[Test]
	public void Push_WhenArgumentIsValid_ShouldAddItemToStack()
	{
		_stack.Push(1);
		_stack.Count.Should().Be(1);
	}

	#endregion Push

	#region Pop

	[Test]
	public void Pop_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
	{
		Action action = () => _stack.Pop();
		action.Should().ThrowExactly<InvalidOperationException>();
	}

	[Test]
	public void Pop_WhenStackHasItems_ShouldReturnFirstItem()
	{
		_stack.Push(1);
		_stack.Push(2);
		_stack.Push(3);
		var item = _stack.Pop();
		item.Should().Be(3);
	}

	[Test]
	public void Pop_WhenStackHasItems_ShouldRemoveFirstItem()
	{
		_stack.Push(1);
		_stack.Push(2);
		_stack.Push(3);
		_ = _stack.Pop();
		_stack.Count.Should().Be(2);
	}

	#endregion Pop

	#region Peek

	[Test]
	public void Peek_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
	{
		Action action = () => _stack.Peek();
		action.Should().ThrowExactly<InvalidOperationException>();
	}

	[Test]
	public void Peek_WhenStackHasItems_ShouldReturnFirstItem()
	{
		_stack.Push(1);
		_stack.Push(2);
		_stack.Push(3);
		var item = _stack.Peek();
		item.Should().Be(3);
	}

	[Test]
	public void Peek_WhenStackHasItems_ShouldNotRemoveAnyItem()
	{
		_stack.Push(1);
		_stack.Push(2);
		_stack.Push(3);
		_ = _stack.Peek();
		_stack.Count.Should().Be(3);
	}

	#endregion Peek
}
