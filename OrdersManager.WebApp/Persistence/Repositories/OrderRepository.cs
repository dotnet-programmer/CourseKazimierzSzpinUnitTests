using Microsoft.EntityFrameworkCore;
using OrdersManager.WebApp.Core;
using OrdersManager.WebApp.Core.Models.Domain;
using OrdersManager.WebApp.Core.Repositories;

namespace OrdersManager.WebApp.Persistence.Repositories;

public class OrderRepository(IApplicationDbContext context) : IOrderRepository
{
	public void AddProduct(Product product)
		=> context.Products.Add(product);

	public Order GetOrderWithProducts(int orderId, string userId)
		=> context.Orders
			.Include(x => x.Products)
			.FirstOrDefault(x => x.Id == orderId && x.UserId == userId)
			?? throw new NullReferenceException("Order doesn't exists.");

	//public Order GetOrderWithProducts(int orderId, string userId)
	//{
	//	var order = context.Orders
	//		.Include(x => x.Products)
	//		.FirstOrDefault(x => x.Id == orderId && x.UserId == userId);
	//	if (order == null)
	//	{
	//		throw new NullReferenceException("Order doesn't exists.");
	//	}
	//	return order;
	//}

	public void UpdateTotalPrice(int orderId, string userId, decimal discount)
	{
		var order = GetOrderWithProducts(orderId, userId);

		if (order.Products == null || order.Products.Count == 0)
		{
			order.TotalPrice = 0;
		}

		var totalPrice = order.Products.Sum(x => x.Price * x.Quantity);
		var totalPriceWithDiscount = totalPrice - discount;
		order.TotalPrice = totalPriceWithDiscount;
	}

	public void DeleteProduct(int id)
	{
		var productToDelete = context.Products.FirstOrDefault(x => x.Id == id) ?? throw new NullReferenceException("Product doesn't exists.");
		context.Products.Remove(productToDelete);
	}

	public Product GetProduct(int orderId, int productId, string userId)
	{
		var order = GetOrderWithProducts(orderId, userId);
		return order.Products.FirstOrDefault(x => x.Id == productId) ?? throw new NullReferenceException("Product doesn't exists.");
	}

	//public Product GetProduct(int orderId, int productId, string userId)
	//{
	//	var order = GetOrderWithProducts(orderId, userId);
	//	var product = order.Products.FirstOrDefault(x => x.Id == productId);
	//	if (product == null)
	//	{
	//		throw new NullReferenceException("Product doesn't exists.");
	//	}
	//	return product;
	//}

	public void UpdateProduct(Product product)
	{
		var productToUpdate = context.Products.FirstOrDefault(x => x.Id == product.Id) ?? throw new NullReferenceException("Product doesn't exists.");
		productToUpdate.Name = product.Name;
		productToUpdate.Price = product.Price;
		productToUpdate.Quantity = product.Quantity;
	}
}