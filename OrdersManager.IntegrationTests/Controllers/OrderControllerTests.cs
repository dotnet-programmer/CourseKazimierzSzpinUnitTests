using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OrdersManager.IntegrationTests.Extensions;
using OrdersManager.IntegrationTests.Helpers;
using OrdersManager.WebApp.Controllers;
using OrdersManager.WebApp.Core;
using OrdersManager.WebApp.Core.Models.Domain;
using OrdersManager.WebApp.Persistence;
using OrdersManager.WebApp.Persistence.Services;

namespace OrdersManager.IntegrationTests.Controllers;

internal class OrderControllerTests
{
	private static readonly IConfigurationBuilder _builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
	private static readonly IConfigurationRoot _config = _builder.Build();

	private readonly DbContextOptions<ApplicationDbContext> _options = new DbContextOptionsBuilder<ApplicationDbContext>()
		.UseSqlServer(_config["ConnectionString"])
		.Options;

	private IApplicationDbContext _context;
	private IUnitOfWork _unitOfWork;
	private OrderController _orderController;
	private ApplicationUser _user;
	private Order _order;
	private Product _product;

	private void Init()
	{
		_context = new ApplicationDbContext(_options);
		_unitOfWork = new UnitOfWork(_context);

		_orderController = new OrderController(new OrderService(_unitOfWork, new DiscountService(_unitOfWork)), null);

		_user = _context.Users.First();
		_orderController.MockCurrentUser(_user.Id, _user.UserName);

		_order = new Order
		{
			UserId = _user.Id,
			FullNumber = "1",
			TotalPrice = 0
		};

		_context.Orders.Add(_order);
		_context.SaveChanges();

		_product = new Product
		{
			Name = "2",
			OrderId = _order.Id,
			Price = 3,
			Quantity = 4
		};
	}

	[TearDown]
	public void Dispose()
		=> _context.Dispose();

	[Test, Isolated]
	public void AddProduct_WhenCalled_ShouldAddProductToDb()
	{
		Init();
		_orderController.AddProduct(_product);

		var productInDb = _context.Products.FirstOrDefault(x => x.Id == _product.Id && x.OrderId == _order.Id);
		productInDb.Should().NotBeNull();
	}

	[Test, Isolated]
	public void AddProduct_WhenCalled_ShouldUpdateOrderTotalPriceInDb()
	{
		Init();
		_orderController.AddProduct(_product);

		var orderInDb = _context.Orders.FirstOrDefault(x => x.Id == _order.Id);
		orderInDb.TotalPrice.Should().Be(2);
	}

	[Test, Isolated]
	public void GetOrder_WhenCalled_ShouldReturnOrderWithProductsFromDb()
	{
		Init();
		_context.Products.Add(_product);
		_context.SaveChanges();

		var result = _orderController.GetOrder(_order.Id);

		var okResult = result as OkObjectResult;
		var resultOrder = okResult?.Value as Order;
		resultOrder.Id.Should().Be(_order.Id);
		resultOrder.Products.FirstOrDefault().Id.Should().Be(_product.Id);
	}
}