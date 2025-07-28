using OrdersManager.WebApp.Core.Services;
using OrdersManager.WebApp.Persistence.Services;

namespace OrdersManager.UnitTests.Persistence.Services;

internal class OrderServiceTests
{
	private Mock<IUnitOfWork> _mockUnitOfWork;
	private Mock<IDiscountService> _mockDiscountService;
	private OrderService _orderService;

	private string _userId;
	private Product _product;
	private Order _order;

	[SetUp]
	public void SetUp()
	{
		_mockUnitOfWork = new();
		_mockDiscountService = new();
		_orderService = new(_mockUnitOfWork.Object, _mockDiscountService.Object);

		_userId = "1";

		_product = new() { Id = 1, OrderId = 1 };

		_order = new()
		{
			Id = 1,
			UserId = _userId,
			TotalPrice = 0,
			Products = new List<Product>()
		};

		_mockUnitOfWork
			.Setup(x => x.Order.GetOrderWithProducts(_product.OrderId, _userId))
			.Returns(_order);

		_mockUnitOfWork
		   .Setup(x => x.Order.GetProduct(_product.OrderId, _product.Id, _userId))
		   .Returns(_product);
	}

	#region AddProduct

	[Test]
	public void AddProduct_WhenOrderDoesntExists_ShouldThrowAnException()
	{
		string badUserId = "2";
		Action action = () => _orderService.AddProduct(badUserId, _product);
		action.Should().Throw<Exception>().WithMessage("Order doesn't exists.");
	}

	[Test]
	public void AddProduct_WhenCalled_ShouldAddProductToDb()
	{
		_orderService.AddProduct(_userId, _product);
		// 2 asercje, bo obie składają się na jedną logikę (asercję logiczną)
		_mockUnitOfWork.Verify(x => x.Order.AddProduct(_product), Times.Once);
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Once);
	}

	[Test]
	public void AddProduct_WhenCalled_ShouldUpdateTotalPriceInDb()
	{
		_orderService.AddProduct(_userId, _product);
		_mockUnitOfWork.Verify(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, 0), Times.Once);
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Once);
	}

	[Test]
	public void AddProduct_WhenCalled_ShouldApplyTotalDiscount()
	{
		var discount = 1;
		_mockDiscountService
			.Setup(x => x.GetDiscount(_product.OrderId, _userId))
			.Returns(discount);
		_orderService.AddProduct(_userId, _product);
		_mockUnitOfWork.Verify(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, discount), Times.Once);
	}

	[Test]
	public void AddProduct_WhenUpdateTotalPriceFails_ShouldNotSaveDataInDb()
	{
		_mockUnitOfWork
			.Setup(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, It.IsAny<decimal>()))
			.Throws(new Exception());
		Action action = () => _orderService.AddProduct(_userId, _product);
		action.Should().Throw<Exception>();
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Never);
	}

	#endregion AddProduct

	#region GetOrderWithProducts

	[Test]
	public void GetOrderWithProducts_WhenCalled_ShouldReturnOrder()
	{
		var result = _orderService.GetOrderWithProducts(_product.OrderId, _userId);

		result.Should().Be(_order);
	}

	[Test]
	public void GetOrderWithProducts_WhenOrderIsNull_ShouldThrowAnException()
	{
		var _badUserId = "2";

		_mockUnitOfWork
			.Setup(x => x.Order.GetOrderWithProducts(_product.OrderId, _badUserId))
			.Returns((Order)null);

		Action action = () => _orderService.GetOrderWithProducts(_product.OrderId, _badUserId);

		action.Should().Throw<Exception>().WithMessage("*Order doesn't exists.*");
	}

	#endregion GetOrderWithProducts

	#region DeleteProduct

	[Test]
	public void DeleteProduct_WhenProductDoesntExists_ShouldThrowAnException()
	{
		var badUserId = "2";

		Action action = () => _orderService.DeleteProduct(_product.OrderId, _product.Id, badUserId);

		action.Should().ThrowExactly<Exception>().WithMessage("*Product doesn't exists.*");
	}

	[Test]
	public void DeleteProduct_WhenCalled_ShouldDeleteProductFromDb()
	{
		_orderService.DeleteProduct(_product.OrderId, _product.Id, _userId);

		_mockUnitOfWork.Verify(x => x.Order.DeleteProduct(_product.Id), Times.Once);
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Once);
	}

	[Test]
	public void DeleteProduct_WhenCalled_ShouldUpdateTotalOrderPriceInDb()
	{
		_orderService.DeleteProduct(_product.OrderId, _product.Id, _userId);

		_mockUnitOfWork.Verify(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, 0), Times.Once);
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Once);
	}

	[Test]
	public void DeleteProduct_WhenCalled_ShouldApplyDiscount()
	{
		var discount = 1;
		_mockDiscountService
			.Setup(x => x.GetDiscount(_product.OrderId, _userId))
			.Returns(discount);

		_orderService.DeleteProduct(_product.OrderId, _product.Id, _userId);

		_mockUnitOfWork.Verify(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, discount), Times.Once);
	}

	[Test]
	public void DeleteProduct_WhenUpdateTotalPiceFails_ShouldNotSaveDataInDb()
	{
		_mockUnitOfWork
			.Setup(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, It.IsAny<decimal>()))
			.Throws(new Exception());

		Action action = () => _orderService.DeleteProduct(_product.OrderId, _product.Id, _userId);

		action.Should().ThrowExactly<Exception>();
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Never);
	}

	#endregion DeleteProduct

	#region UpdateProduct

	[Test]
	public void UpdateProduct_WhenOrderDoesntExists_ShouldThrowAnException()
	{
		var badUserId = "2";

		Action action = () => _orderService.UpdateProduct(badUserId, _product);

		action.Should().ThrowExactly<Exception>().WithMessage("*Order doesn't exists.*");
	}

	[Test]
	public void UpdateProduct_WhenCalled_ShouldUpdateProductInDb()
	{
		_orderService.UpdateProduct(_userId, _product);

		_mockUnitOfWork.Verify(x => x.Order.UpdateProduct(_product), Times.Once);
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Once);
	}

	[Test]
	public void UpdateProduct_WhenCalled_ShouldUpdateTotalOrderPriceInDb()
	{
		_orderService.UpdateProduct(_userId, _product);

		_mockUnitOfWork.Verify(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, 0), Times.Once);
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Once);
	}

	[Test]
	public void UpdateProduct_WhenCalled_ShouldApplyDiscount()
	{
		var discount = 1;
		_mockDiscountService
			.Setup(x => x.GetDiscount(_product.OrderId, _userId))
			.Returns(discount);

		_orderService.UpdateProduct(_userId, _product);

		_mockUnitOfWork.Verify(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, discount), Times.Once);
	}

	[Test]
	public void UpdateProduct_WhenUpdateTotalPriceFails_ShouldNotSaveDateInDb()
	{
		_mockUnitOfWork
			.Setup(x => x.Order.UpdateTotalPrice(_product.OrderId, _userId, It.IsAny<decimal>()))
			.Throws(new Exception());

		Action action = () => _orderService.UpdateProduct(_userId, _product);

		action.Should().ThrowExactly<Exception>();
		_mockUnitOfWork.Verify(x => x.Complete(), Times.Never);
	}

	#endregion UpdateProduct
}