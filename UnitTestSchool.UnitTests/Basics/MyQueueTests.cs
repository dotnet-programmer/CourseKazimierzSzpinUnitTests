namespace UnitTestSchool.UnitTests.Basics;

internal class MyQueueTests
{
	private MyQueue<int?> _queue;

	[SetUp]
	public void SetUp() => _queue = new();

	#region Enqueue

	[Test]
	public void Enqueue_WhenArgumentIsNull_ShouldThrowArgumentNullException()
	{
		Action action = () => _queue.Enqueue(null);
		action.Should().ThrowExactly<ArgumentNullException>().WithMessage("*value*");
	}

	[Test]
	public void Enqueue_WhenArgumentIsValid_ShouldAddItemToQueue()
	{
		_queue.Enqueue(1);
		_queue.Count.Should().Be(1);
	}

	#endregion Enqueue

	#region Peek

	[Test]
	public void Peek_WhenQueueIsEmpty_ShouldThrowInvalidOperationException()
	{
		Action action = () => _queue.Peek();
		action.Should().ThrowExactly<InvalidOperationException>();
	}

	[Test]
	public void Peek_WhenQueueHasItems_ShouldReturnFirstItem()
	{
		_queue.Enqueue(1);
		_queue.Enqueue(2);
		_queue.Enqueue(3);
		var item = _queue.Peek();
		item.Should().Be(1);
	}

	[Test]
	public void Peek_WhenQueueHasItems_ShouldNotRemoveAnyItem()
	{
		_queue.Enqueue(1);
		_queue.Enqueue(2);
		_queue.Enqueue(3);
		_ = _queue.Peek();
		_queue.Count.Should().Be(3);
	}

	#endregion Peek

	#region Dequeue

	[Test]
	public void Dequeue_WhenQueueIsEmpty_ShouldThrowInvalidOperationException()
	{
		Action action = () => _queue.Dequeue();
		action.Should().ThrowExactly<InvalidOperationException>();
	}

	[Test]
	public void Dequeue_WhenQueueHasItems_ShouldReturnFirstItem()
	{
		_queue.Enqueue(1);
		_queue.Enqueue(2);
		_queue.Enqueue(3);
		var item = _queue.Dequeue();
		item.Should().Be(1);
	}

	[Test]
	public void Dequeue_WhenQueueHasItems_ShouldRemoveFirstItem()
	{
		_queue.Enqueue(1);
		_queue.Enqueue(2);
		_queue.Enqueue(3);
		_ = _queue.Dequeue();
		_queue.Count.Should().Be(2);
	}

	#endregion Dequeue
}