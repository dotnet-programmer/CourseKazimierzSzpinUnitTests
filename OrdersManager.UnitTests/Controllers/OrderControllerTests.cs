using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using Moq;
using NLog;
using NUnit.Framework;
using OrdersManager.Controllers;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Core.Services;
using OrdersManager.UnitTests.Extensions;

namespace OrdersManager.UnitTests.Controllers
{
	internal class OrderControllerTests
	{
		private const string ExceptionMessage = "Service failure";

		private string _userId;
		private Product _product;
		private Mock<IOrderService> _mockOrderService;
		private Mock<ILogger> _mockLogger;
		private OrderController _orderController;
		private Order _order;
		private Exception _exception;

		[SetUp]
		public void Setup()
		{
			_userId = "1";
			_product = new Product { Id = 1, OrderId = 1, Name = "Test Product", Price = 10.0m };
			_mockOrderService = new Mock<IOrderService>();
			_mockLogger = new Mock<ILogger>();
			_orderController = new OrderController(_mockOrderService.Object, _mockLogger.Object);
			_orderController.MockCurrentUser(_userId, "test@user.name");
			_order = new Order { Id = 1, Products = new List<Product> { _product }, UserId = _userId };
			_exception = new Exception(ExceptionMessage);
		}

		#region AddProduct

		[Test]
		public void AddProduct_WhenProductIsNull_ShouldReturnBadRequest()
		{
			var result = _orderController.AddProduct(null);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Test]
		public void AddProduct_WhenOrderIdIsLowerThan1_ShouldReturnBadRequest()
		{
			_product.OrderId = 0; // Set OrderId to an invalid value

			var result = _orderController.AddProduct(_product);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Test]
		public void AddProduct_WhenCalled_ShouldAddProduct()
		{
			var result = _orderController.AddProduct(_product);

			_mockOrderService.Verify(x => x.AddProduct(_userId, _product), Times.Once);
		}

		[Test]
		public void AddProduct_WhenCalled_ShouldReturnOkResult()
		{
			var result = _orderController.AddProduct(_product);

			result.Should().BeOfType<OkResult>();
		}

		[Test]
		public void AddProduct_WhenOrderServiceFails_ShouldLogError()
		{
			_mockOrderService
				.Setup(x => x.AddProduct(_userId, _product))
				.Throws(_exception);

			var result = _orderController.AddProduct(_product);

			_mockLogger.Verify(x => x.Error(ExceptionMessage), Times.Once);
		}

		[Test]
		public void AddProduct_WhenOrderServiceFails_ShouldReturnBadRequest()
		{
			_mockOrderService
				.Setup(x => x.AddProduct(_userId, _product))
				.Throws(_exception);

			var result = _orderController.AddProduct(_product);

			result.Should().BeOfType<BadRequestResult>();
		}

		#endregion AddProduct

		#region GetOrder

		[Test]
		public void GetOrder_WhenOrderIdIsLowerThan1_ShouldReturnBadRequest()
		{
			var result = _orderController.GetOrder(0);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Test]
		public void GetOrder_WhenCalled_ShouldGetOrderFromService()
		{
			var result = _orderController.GetOrder(_order.Id);

			_mockOrderService.Verify(x => x.GetOrderWithProducts(_order.Id, _userId), Times.Once);
		}

		[Test]
		public void GetOrder_WhenCalled_ShouldReturnOkResultWithOrder()
		{
			_mockOrderService
				.Setup(x => x.GetOrderWithProducts(_order.Id, _userId))
				.Returns(_order);

			var result = _orderController.GetOrder(_order.Id);

			result.Should().BeOfType<OkNegotiatedContentResult<Order>>();
		}

		[Test]
		public void GetOrder_WhenOrderServiceFails_ShouldLogError()
		{
			_mockOrderService
				.Setup(x => x.GetOrderWithProducts(_order.Id, _userId))
				.Throws(_exception);

			var result = _orderController.GetOrder(_order.Id);

			_mockLogger.Verify(x => x.Error(ExceptionMessage), Times.Once);
		}

		[Test]
		public void GetOrder_WhenOrderServiceFails_ShouldReturnBadRequest()
		{
			_mockOrderService
				.Setup(x => x.GetOrderWithProducts(_order.Id, _userId))
				.Throws(_exception);

			var result = _orderController.GetOrder(_order.Id);

			result.Should().BeOfType<BadRequestResult>();
		}

		#endregion GetOrder

		#region DeleteProduct

		[Test]
		public void DeleteProduct_WhenCalled_ShouldReturnOkResult()
		{
			var result = _orderController.DeleteProduct(_order.Id, _product.Id);

			result.Should().BeOfType<OkResult>();
		}

		[Test]
		public void DeleteProduct_WhenCalled_ShouldDeleteProduct()
		{
			var result = _orderController.DeleteProduct(_order.Id, _product.Id);

			_mockOrderService.Verify(x => x.DeleteProduct(_order.Id, _product.Id, _userId), Times.Once);
		}

		[Test]
		public void DeleteProduct_WhenProductIdIsLowerThan1_ShouldReturnBadRequest()
		{
			var result = _orderController.DeleteProduct(_order.Id, 0);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Test]
		public void DeleteProduct_WhenOrderIdIsLowerThan1_ShouldReturnBadRequest()
		{
			var result = _orderController.DeleteProduct(0, _product.Id);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Test]
		public void DeleteProduct_WhenOrderServiceFails_ShouldLogError()
		{
			_mockOrderService
				.Setup(x => x.DeleteProduct(_order.Id, _product.Id, _userId))
				.Throws(_exception);

			var result = _orderController.DeleteProduct(_order.Id, _product.Id);

			_mockLogger.Verify(x => x.Error(ExceptionMessage), Times.Once);
		}

		[Test]
		public void DeleteProduct_WhenOrderServiceFails_ShouldReturnBadRequest()
		{
			_mockOrderService
				.Setup(x => x.DeleteProduct(_order.Id, _product.Id, _userId))
				.Throws(_exception);

			var result = _orderController.DeleteProduct(_order.Id, _product.Id);

			result.Should().BeOfType<BadRequestResult>();
		}

		#endregion DeleteProduct

		#region UpdateProduct

		[Test]
		public void UpdateProduct_WhenCalled_ShouldReturnOkRequest()
		{
			var result = _orderController.UpdateProduct(_product);

			result.Should().BeOfType<OkResult>();
		}

		[Test]
		public void UpdateProduct_WhenProductIsNull_ShouldReturnBadRequest()
		{
			var result = _orderController.UpdateProduct(null);

			result.Should().BeOfType<BadRequestResult>();
		}

		[Test]
		public void UpdateProduct_WhenCalled_ShouldUpdateProduct()
		{
			var result = _orderController.UpdateProduct(_product);

			_mockOrderService
				.Verify(x => x.UpdateProduct(_userId, _product), Times.Once);
		}

		[Test]
		public void UpdateProduct_WhenOrdersServiceFails_ShouldLogError()
		{
			_mockOrderService
				.Setup(x => x.UpdateProduct(_userId, _product))
				.Throws(_exception);

			var result = _orderController.UpdateProduct(_product);

			_mockLogger.Verify(x => x.Error(ExceptionMessage), Times.Once);
		}

		[Test]
		public void UpdateProduct_WhenOrdersServiceFails_ShouldReturnBadRequest()
		{
			_mockOrderService
				.Setup(x => x.UpdateProduct(_userId, _product))
				.Throws(_exception);

			var result = _orderController.UpdateProduct(_product);

			result.Should().BeOfType<BadRequestResult>();
		}

		#endregion UpdateProduct
	}
}