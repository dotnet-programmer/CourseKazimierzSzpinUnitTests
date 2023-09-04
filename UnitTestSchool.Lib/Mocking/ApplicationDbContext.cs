using Microsoft.EntityFrameworkCore;

namespace UnitTestSchool.Lib.Mocking;

public class ApplicationDbContext : DbContext
{
	public DbSet<Task> Tasks { get; set; }
	public DbSet<RandomNumber> Numbers { get; internal set; }

	//public ApplicationDbContext() : base("connectionString")
	//{

	//}
}
