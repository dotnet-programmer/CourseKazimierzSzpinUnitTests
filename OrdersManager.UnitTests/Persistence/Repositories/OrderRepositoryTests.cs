using Microsoft.EntityFrameworkCore;
using OrdersManager.UnitTests.Extensions;
using OrdersManager.WebApp.Persistence.Repositories;

namespace OrdersManager.UnitTests.Persistence.Repositories;

internal class OrderRepositoryTests
{
	private Mock<IApplicationDbContext> _mockContext;
	private Mock<DbSet<Product>> _mockProducts;
	private Mock<DbSet<Order>> _mockOrders;
	private OrderRepository _orderRepository;
	private Product _product;
	private Product _product2;
	private List<Order> _orders;
	private List<Product> _products;

	[SetUp]
	public void Setup()
	{
		_mockContext = new();
		_mockProducts = new();
		_mockOrders = new();
		_orderRepository = new(_mockContext.Object);

		_product = new() { Id = 1, Price = 1, Quantity = 3, OrderId = 1 };
		_product2 = new() { Id = 2, Price = 2, Quantity = 33 };

		_orders = [
			new Order { Id = 1, UserId = "1", Products = new List<Product> { _product } },
			new Order { Id = 2, UserId = "2", Products = new List<Product> { _product2 } },
			new Order { Id = 3, UserId = "1", Products = new List<Product> { _product2 } }
		];

		_products = [_product, _product2];

		_mockOrders.SetSource(_orders);
		_mockProducts.SetSource(_products);

		_mockContext
			.Setup(x => x.Orders)
			.Returns(_mockOrders.Object);

		_mockContext
			.Setup(x => x.Products)
			.Returns(_mockProducts.Object);
	}

	[Test]
	public void AddProduct_WhenCalled_ShouldAddProductToDb()
	{
		_orderRepository.AddProduct(_product);

		_mockContext.Verify(x => x.Products.Add(_product), Times.Once);
	}

	[Test]
	public void GetOrderWithProducts_WhenOrderDoesntExists_ShouldThrowNullReferenceException()
	{
		var badOderId = 3;

		Action action = () => _orderRepository.GetOrderWithProducts(badOderId, It.IsAny<string>());

		action.Should().ThrowExactly<NullReferenceException>().WithMessage("*Order doesn't exists.*");
	}

	[Test]
	public void GetOrderWithProducts_WhenCalled_ShouldReturnCorrectOrder()
	{
		var result = _orderRepository.GetOrderWithProducts(_orders.First().Id, _orders.First().UserId);

		result.Should().Be(_orders.First());
	}

	[Test]
	public void DeleteProduct_WhenCalled_ShouldDeleteProductFromDb()
	{
		_orderRepository.DeleteProduct(_product.Id);

		_mockContext.Verify(x => x.Products.Remove(_product), Times.Once);
	}

	[Test]
	public void DeleteProduct_WhenProductDoesntExists_ShouldThrowNullReferenceException()
	{
		var badProductId = 3;

		Action action = () => _orderRepository.DeleteProduct(badProductId);

		action.Should().ThrowExactly<NullReferenceException>().WithMessage("*Product doesn't exists.*");
	}

	[Test]
	public void GetProduct_WhenCalled_ShouldReturnCorrectProduct()
	{
		var result = _orderRepository.GetProduct(_product.OrderId, _product.Id, _orders[0].UserId);

		result.Should().Be(_product);
	}

	[Test]
	public void GetProduct_WhenProductDoesntExists_ShouldThrowNullReferenceException()
	{
		Action action = () => _orderRepository.GetProduct(_product.OrderId, It.IsAny<int>(), _orders[0].UserId);

		action.Should().ThrowExactly<NullReferenceException>().WithMessage("*Product doesn't exists.*");
	}

	[Test]
	public void UpdateProduct_WhenCalled_ShouldUpdateProductInDb()
	{
		var newProduct = new Product
		{
			Id = _product.Id,
			Name = _product.Name + "1",
			Price = _product.Price + 1,
			Quantity = _product.Quantity + 1
		};

		_orderRepository.UpdateProduct(newProduct);

		_product.Name.Should().Be(newProduct.Name);
		_product.Price.Should().Be(newProduct.Price);
		_product.Quantity.Should().Be(newProduct.Quantity);
	}

	[Test]
	public void UpdateProduct_WhenProductDoesntExists_ShouldThrowNullReferenceException()
	{
		var badProductId = 3;

		Action action = () => _orderRepository.UpdateProduct(new Product { Id = badProductId });

		action.Should().ThrowExactly<NullReferenceException>().WithMessage("*Product doesn't exists.*");
	}
}