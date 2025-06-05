using System;
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

		[SetUp]
		public void Setup()
		{
			_userId = "1";
			_product = new Product
			{
				Id = 1,
				Name = "Test Product",
				Price = 10.0m,
				OrderId = 1
			};
			_mockOrderService = new Mock<IOrderService>();
			_mockLogger = new Mock<ILogger>();
			_orderController = new OrderController(_mockOrderService.Object, _mockLogger.Object);
			_orderController.MockCurrentUser(_userId, "test@user.name");
		}

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
				.Throws(new Exception(ExceptionMessage));
			var result = _orderController.AddProduct(_product);
			_mockLogger.Verify(x => x.Error(ExceptionMessage), Times.Once);
		}

		[Test]
		public void AddProduct_WhenOrderServiceFails_ShouldReturnBadRequest()
		{
			_mockOrderService
				.Setup(x => x.AddProduct(_userId, _product))
				.Throws(new Exception(ExceptionMessage));
			var result = _orderController.AddProduct(_product);
			result.Should().BeOfType<BadRequestResult>();
		}
	}
}