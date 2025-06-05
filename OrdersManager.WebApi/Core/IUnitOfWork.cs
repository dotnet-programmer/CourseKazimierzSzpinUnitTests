using OrdersManager.Core.Repositories;

namespace OrdersManager.Core
{
	public interface IUnitOfWork
	{
		void Complete();

		IOrderRepository Order { get; }
		IUserRepository User { get; }
	}
}