using System.Linq;
using System.Web.Http.Results;
using FluentAssertions;
using NUnit.Framework;
using OrdersManager.Controllers;
using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;
using OrdersManager.IntegrationTests.Extensions;
using OrdersManager.IntegrationTests.Helpers;
using OrdersManager.Models;
using OrdersManager.Persistence;
using OrdersManager.Persistence.Services;

namespace OrdersManager.IntegrationTests.Controllers
{
	internal class OrderControllerTests
	{
		private IApplicationDbContext _context;
		private IUnitOfWork _unitOfWork;
		private OrderController _orderController;
		private ApplicationUser _user;
		private Order _order;
		private Product _product;

		private void Init()
		{
			_context = new ApplicationDbContext();
			_unitOfWork = new UnitOfWork(_context);

			_orderController = new OrderController(
				new OrderService(_unitOfWork, new DiscountService(_unitOfWork)), null);

			_user = _context.Users.First();
			_orderController.MockCurrentUser(_user.Id, _user.UserName);

			_order = new Order
			{
				UserId = _user.Id,
				FullNumber = "1",
				TotalPrice = 0
			};

			_product = new Product
			{
				Name = "2",
				OrderId = _order.Id,
				Price = 3,
				Quantity = 4
			};
		}

		[Test, Isolated]
		public void AddProduct_WhenCalled_ShouldAddProductToDb()
		{
			Init();
			_context.Orders.Add(_order);
			_context.SaveChanges();

			_orderController.AddProduct(_product);

			var productInDb = _context.Products.FirstOrDefault(x => x.Id == _product.Id && x.OrderId == _order.Id);
			productInDb.Should().NotBeNull();
		}

		[Test, Isolated]
		public void AddProduct_WhenCalled_ShouldUpdateOrderTotalPriceInDb()
		{
			Init();
			_context.Orders.Add(_order);
			_context.SaveChanges();

			_orderController.AddProduct(_product);

			var orderInDb = _context.Orders.FirstOrDefault(x => x.Id == _order.Id);
			orderInDb.TotalPrice.Should().Be(2);
		}

		[Test, Isolated]
		public void GetOrder_WhenCalled_ShouldReturnOrderWithProductsFromDb()
		{
			Init();
			_context.Orders.Add(_order);
			_context.Products.Add(_product);
			_context.SaveChanges();

			var result = _orderController.GetOrder(_order.Id);

			var resultOrder = (result as OkNegotiatedContentResult<Order>).Content;
			resultOrder.Id.Should().Be(_order.Id);
			resultOrder.Products.FirstOrDefault().Id.Should().Be(_product.Id);
		}

		[TearDown]
		public void Dispose()
			=> _context.Dispose();
	}
}