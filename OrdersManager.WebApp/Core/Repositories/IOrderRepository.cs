using OrdersManager.WebApp.Core.Models.Domain;

namespace OrdersManager.WebApp.Core.Repositories;

public interface IOrderRepository
{
	void UpdateTotalPrice(int orderId, string userId, decimal discount);
	Order GetOrderWithProducts(int orderId, string userId);
	void AddProduct(Product product);
	void DeleteProduct(int id);
	Product GetProduct(int orderId, int productId, string userId);
	void UpdateProduct(Product product);
}