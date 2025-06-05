using System;
using System.Collections.Generic;
using System.Web.Http;
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
	internal class OrderControllerGetOrderTests
	{
		private const string ExceptionMessage = "Service failure";

		private int _orderId;
		private string _userId;
		private Mock<IOrderService> _mockOrderService;
		private Mock<ILogger> _mockLogger;
		private OrderController _orderController;

		[SetUp]
		public void Setup()
		{
			_orderId = 1;
			_userId = "1";
			_mockOrderService = new Mock<IOrderService>();
			_mockLogger = new Mock<ILogger>();
			_orderController = new OrderController(_mockOrderService.Object, _mockLogger.Object);
			_orderController.MockCurrentUser(_userId, "test@user.name");
		}

		[Test]
		public void GetOrder_WhenOrderIdIsLowerThan1_ShouldReturnBadRequest()
		{
			var result = _orderController.GetOrder(0);
			result.Should().BeOfType<BadRequestResult>();
		}

		[Test]
		public void GetOrder_WhenCalled_ShouldCalledOnce()
		{
			var result = _orderController.GetOrder(_orderId);
			_mockOrderService.Verify(x => x.GetOrderWithProducts(_orderId, _userId), Times.Once);
		}

		[Test]
		public void GetOrder_WhenCalled_ShouldReturnOkResult()
		{
			var result = _orderController.GetOrder(_orderId);
			result.Should().BeOfType<OkNegotiatedContentResult<Order>>();
		}

		[Test]
		public void GetOrder_WhenCalled_ShouldReturnOrder()
		{
			_mockOrderService
				.Setup(x => x.GetOrderWithProducts(_orderId, _userId))
				.Returns(new Order
				{
					Id = _orderId,
					UserId = _userId,
					Products = new List<Product>()
				});

			IHttpActionResult actionResult = _orderController.GetOrder(_orderId);
			var contentResult = actionResult as OkNegotiatedContentResult<Order>;
			contentResult.Should().NotBeNull();
			contentResult.Content.Should().NotBeNull();
			contentResult.Content.Should().BeOfType<Order>();
		}

		[Test]
		public void GetOrder_WhenOrderServiceFails_ShouldLogError()
		{
			_mockOrderService
				.Setup(x => x.GetOrderWithProducts(_orderId, _userId))
				.Throws(new Exception(ExceptionMessage));
			var result = _orderController.GetOrder(_orderId);
			_mockLogger.Verify(x => x.Error(ExceptionMessage), Times.Once);
		}

		[Test]
		public void GetOrder_WhenOrderServiceFails_ShouldReturnBadRequest()
		{
			_mockOrderService
				.Setup(x => x.GetOrderWithProducts(_orderId, _userId))
				.Throws(new Exception(ExceptionMessage));
			var result = _orderController.GetOrder(_orderId);
			result.Should().BeOfType<BadRequestResult>();
		}
	}
}