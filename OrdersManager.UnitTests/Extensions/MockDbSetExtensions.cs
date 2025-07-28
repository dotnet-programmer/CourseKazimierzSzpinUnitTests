using Microsoft.EntityFrameworkCore;

namespace OrdersManager.UnitTests.Extensions;

public static class MockDbSetExtensions
{
	public static void SetSource<T>(this Mock<DbSet<T>> mockSet, IEnumerable<T> source) where T : class
	{
		var queryable = source.AsQueryable();
		mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
		mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
		mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
		mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
	}
}