using OrdersManager.Core.Repositories;

namespace OrdersManager.Core
{
	public interface IUnitOfWork
	{
		IOrderRepository Order { get; }
		IUserRepository User { get; }

		void Complete();
	}
}