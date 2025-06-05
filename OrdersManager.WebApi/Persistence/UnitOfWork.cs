using OrdersManager.Core;
using OrdersManager.Core.Repositories;
using OrdersManager.Persistence.Repositories;

namespace OrdersManager.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IApplicationDbContext _context;

		public UnitOfWork(IApplicationDbContext context)
		{
			_context = context;
			Order = new OrderRepository(context);
			User = new UserRepository(context);
		}

		public IOrderRepository Order { get; }
		public IUserRepository User { get; }

		public void Complete()
			=> _context.SaveChanges();
	}
}