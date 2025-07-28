using System;
using System.Data.Entity;
using System.Linq;
using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Core.Repositories;

namespace OrdersManager.Persistence.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IApplicationDbContext _context;

		public OrderRepository(IApplicationDbContext context)
			=> _context = context;

		public void AddProduct(Product product)
			=> _context.Products.Add(product);

		public Order GetOrderWithProducts(int orderId, string userId)
			=> _context.Orders
				.Include(x => x.Products)
				.FirstOrDefault(x => x.Id == orderId && x.UserId == userId)
			?? throw new NullReferenceException("Order doesn't exists.");

		//public Order GetOrderWithProducts(int orderId, string userId)
		//{
		//	var order = _context.Orders
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

			if (order.Products == null || !order.Products.Any())
			{
				order.TotalPrice = 0;
			}

			var totalPrice = order.Products.Sum(x => x.Price * x.Quantity);
			var totalPriceWithDiscount = totalPrice - discount;
			order.TotalPrice = totalPriceWithDiscount;
		}

		public void DeleteProduct(int id)
		{
			var productToDelete = _context.Products.FirstOrDefault(x => x.Id == id) ?? throw new NullReferenceException("Product doesn't exists.");
			_context.Products.Remove(productToDelete);
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
			var productToUpdate = _context.Products.FirstOrDefault(x => x.Id == product.Id) ?? throw new NullReferenceException("Product doesn't exists.");
			productToUpdate.Name = product.Name;
			productToUpdate.Price = product.Price;
			productToUpdate.Quantity = product.Quantity;
		}
	}
}