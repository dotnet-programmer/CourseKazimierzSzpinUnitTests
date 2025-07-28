using OrdersManager.WebApp.Core;
using OrdersManager.WebApp.Core.Models.Domain;
using OrdersManager.WebApp.Core.Services;

namespace OrdersManager.WebApp.Persistence.Services;

public class OrderService(IUnitOfWork unitOfWork, IDiscountService discountService) : IOrderService
{
	public void AddProduct(string userId, Product product)
	{
		var order = unitOfWork.Order.GetOrderWithProducts(product.OrderId, userId) ?? throw new Exception("Order doesn't exists.");
		unitOfWork.Order.AddProduct(product);
		var discount = discountService.GetDiscount(product.OrderId, userId);
		unitOfWork.Order.UpdateTotalPrice(product.OrderId, userId, discount);
		unitOfWork.Complete();
	}

	public Order GetOrderWithProducts(int orderId, string userId)
		=> unitOfWork.Order.GetOrderWithProducts(orderId, userId) ?? throw new Exception("Order doesn't exists.");

	//public Order GetOrderWithProducts(int orderId, string userId)
	//{
	//	var order = unitOfWork.Order.GetOrderWithProducts(orderId, userId);
	//	if (order == null)
	//	{
	//		throw new Exception("Order doesn't exists.");
	//	}
	//	return order;
	//}

	public void DeleteProduct(int orderId, int productId, string userId)
	{
		var product = unitOfWork.Order.GetProduct(orderId, productId, userId) ?? throw new Exception("Product doesn't exists.");
		unitOfWork.Order.DeleteProduct(productId);
		UpdateOrderTotalPrice(userId, product);
		unitOfWork.Complete();
	}

	public void UpdateProduct(string userId, Product product)
	{
		var order = unitOfWork.Order.GetOrderWithProducts(product.OrderId, userId) ?? throw new Exception("Order doesn't exists.");
		unitOfWork.Order.UpdateProduct(product);
		UpdateOrderTotalPrice(userId, product);
		unitOfWork.Complete();
	}

	private void UpdateOrderTotalPrice(string userId, Product product)
	{
		var discount = discountService.GetDiscount(product.OrderId, userId);
		unitOfWork.Order.UpdateTotalPrice(product.OrderId, userId, discount);
	}
}