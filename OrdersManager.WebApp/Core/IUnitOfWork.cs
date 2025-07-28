using OrdersManager.WebApp.Core.Repositories;

namespace OrdersManager.WebApp.Core;

public interface IUnitOfWork
{
	IOrderRepository Order { get; }
	IUserRepository User { get; }

	void Complete();
}