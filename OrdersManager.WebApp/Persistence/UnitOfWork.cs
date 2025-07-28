using OrdersManager.WebApp.Core;
using OrdersManager.WebApp.Core.Repositories;
using OrdersManager.WebApp.Persistence.Repositories;

namespace OrdersManager.WebApp.Persistence;

public class UnitOfWork(IApplicationDbContext context) : IUnitOfWork
{
	public IOrderRepository Order { get; } = new OrderRepository(context);
	public IUserRepository User { get; } = new UserRepository(context);

	public void Complete()
		=> context.SaveChanges();
}