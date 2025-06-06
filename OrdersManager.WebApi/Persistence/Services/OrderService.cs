using System;
using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Core.Services;

namespace OrdersManager.Persistence.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IDiscountService _discountService;

		public OrderService(IUnitOfWork unitOfWork, IDiscountService discountService)
		{
			_unitOfWork = unitOfWork;
			_discountService = discountService;
		}

		public void AddProduct(string userId, Product product)
		{
			var order = _unitOfWork.Order.GetOrderWithProducts(product.OrderId, userId) ?? throw new Exception("Order doesn't exists.");
			_unitOfWork.Order.AddProduct(product);
			var discount = _discountService.GetDiscount(product.OrderId, userId);
			_unitOfWork.Order.UpdateTotalPrice(product.OrderId, userId, discount);
			_unitOfWork.Complete();
		}

		public Order GetOrderWithProducts(int orderId, string userId)
			=> _unitOfWork.Order.GetOrderWithProducts(orderId, userId) ?? throw new Exception("Order doesn't exists.");

		//public Order GetOrderWithProducts(int orderId, string userId)
		//{
		//	var order = _unitOfWork.Order.GetOrderWithProducts(orderId, userId);
		//	if (order == null)
		//	{
		//		throw new Exception("Order doesn't exists.");
		//	}
		//	return order;
		//}

		public void DeleteProduct(int orderId, int productId, string userId)
		{
			var product = _unitOfWork.Order.GetProduct(orderId, productId, userId) ?? throw new Exception("Product doesn't exists.");
			_unitOfWork.Order.DeleteProduct(productId);
			UpdateOrderTotalPrice(userId, product);
			_unitOfWork.Complete();
		}

		public void UpdateProduct(string userId, Product product)
		{
			var order = _unitOfWork.Order.GetOrderWithProducts(product.OrderId, userId) ?? throw new Exception("Order doesn't exists.");
			_unitOfWork.Order.UpdateProduct(product);
			UpdateOrderTotalPrice(userId, product);
			_unitOfWork.Complete();
		}

		private void UpdateOrderTotalPrice(string userId, Product product)
		{
			var discount = _discountService.GetDiscount(product.OrderId, userId);
			_unitOfWork.Order.UpdateTotalPrice(product.OrderId, userId, discount);
		}
	}
}