using OrdersManager.Core.Models.Domain;

namespace OrdersManager.Core.Repositories
{
	public interface IOrderRepository
	{
		void UpdateTotalPrice(int orderId, string userId, decimal discount);

		Order GetOrderWithProducts(int orderId, string userId);

		void AddProduct(Product product);
	}
}