namespace UnitTestSchool.Lib.Mocking;

public class Task
{
	public int Id { get; set; }
	public string Title { get; set; }
	public bool IsClosed { get; set; }
	public int UserId { get; set; }
	public User User { get; set; }
}
