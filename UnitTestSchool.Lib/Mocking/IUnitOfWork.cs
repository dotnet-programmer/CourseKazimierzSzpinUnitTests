namespace UnitTestSchool.Lib.Mocking;

// zawiera tylko repozytoria + Complete()
public interface IUnitOfWork
{
	ITaskRepository Task { get; }

	void Complete();
}