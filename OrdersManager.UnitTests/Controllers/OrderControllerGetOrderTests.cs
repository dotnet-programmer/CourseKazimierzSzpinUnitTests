using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersManager.UnitTests.Extensions;
using OrdersManager.WebApp.Controllers;
using OrdersManager.WebApp.Core.Services;

namespace OrdersManager.UnitTests.Controllers;

internal class OrderControllerGetOrderTests
{
	private const string ExceptionMessage = "Service failure";

	private int _orderId;
	private string _userId;
	private Mock<IOrderService> _mockOrderService;
	private Mock<ILogger<OrderController>> _mockLogger;
	private OrderController _orderController;

	[SetUp]
	public void Setup()
	{
		_orderId = 1;
		_userId = "1";
		_mockOrderService = new();
		_mockLogger = new();
		_orderController = new(_mockOrderService.Object, _mockLogger.Object);
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

		result.Should().BeOfType<OkObjectResult>();
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

		IActionResult result = _orderController.GetOrder(_orderId);

		result.Should().BeOfType<OkObjectResult>();

		var okResult = result as OkObjectResult;
		okResult.Should().NotBeNull();
		okResult.Value.Should().BeOfType<Order>();
	}

	[Test]
	public void GetOrder_WhenOrderServiceFails_ShouldLogError()
	{
		_mockOrderService
			.Setup(x => x.GetOrderWithProducts(_orderId, _userId))
			.Throws(new Exception(ExceptionMessage));

		var result = _orderController.GetOrder(_orderId);

		//_mockLogger.Verify(x => x.LogError(ExceptionMessage), Times.Once);

		// Example: Verifying a call to LogError with a specific message
		//_mockLogger.Verify(
		//	x => x.Log(
		//		LogLevel.Error,
		//		It.IsAny<EventId>(),
		//		It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(ExceptionMessage)),
		//		It.IsAny<Exception>(),
		//		It.IsAny<Func<It.IsAnyType, Exception, string>>()),
		//	Times.Once);

		_mockLogger.VerifyLog(LogLevel.Error, ExceptionMessage, Times.Once());
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