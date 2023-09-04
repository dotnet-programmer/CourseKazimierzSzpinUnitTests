namespace UnitTestSchool.Lib.Mocking;

public interface IUnitOfWork
{
	ITaskRepository Task { get; }

	void Complete();
}