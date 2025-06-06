using OrdersManager.Core.Models.Domain;

namespace OrdersManager.Core.Services
{
	public interface IOrderService
	{
		void AddProduct(string userId, Product product);

		Order GetOrderWithProducts(int orderId, string userId);

		void DeleteProduct(int orderId, int productId, string _userId);

		void UpdateProduct(string userId, Product product);
	}
}