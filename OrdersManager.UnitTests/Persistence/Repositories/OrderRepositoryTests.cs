using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Persistence.Repositories;
using OrdersManager.UnitTests.Extensions;

namespace OrdersManager.UnitTests.Persistence.Repositories
{
	internal class OrderRepositoryTests
	{
		private Mock<DbSet<Product>> _mockProducts;
		private Mock<IApplicationDbContext> _mockContext;
		private OrderRepository _orderRepository;
		private Product _product;
		private Mock<DbSet<Order>> _mockOrders;
		private List<Order> _orders;

		[SetUp]
		public void Setup()
		{
			_mockProducts = new Mock<DbSet<Product>>();
			_mockContext = new Mock<IApplicationDbContext>();
			_orderRepository = new OrderRepository(_mockContext.Object);
			_mockOrders = new Mock<DbSet<Order>>();
			_product = new Product
			{
				Id = 1,
				Name = "Test Product",
				Price = 100,
				OrderId = 1
			};

			_orders = new List<Order>
			{
				new Order { Id = 1, UserId = "1" },
				new Order { Id = 2, UserId = "2" },
				new Order { Id = 3, UserId = "1" },
			};
			_mockOrders.SetSource(_orders);

			_mockContext.Setup(x => x.Orders).Returns(_mockOrders.Object);
			_mockContext.Setup(x => x.Products).Returns(_mockProducts.Object);
		}

		[Test]
		public void AddProduct_WhenCalled_ShouldAddProductToDb()
		{
			_orderRepository.AddProduct(_product);
			_mockContext.Verify(x => x.Products.Add(_product), Times.Once);
		}

		[Test]
		public void GetOrderWithProducts_WhenOrderDoesntExists_ShouldThrownNullReferenceException()
		{
			string badUserId = "3";
			Action action = () => _orderRepository.GetOrderWithProducts(It.IsAny<int>(), badUserId);
			action.Should().ThrowExactly<NullReferenceException>().WithMessage("*Order doesn't exists.*");
		}

		[Test]
		public void GetOrderWithProducts_WhenCalled_ShouldReturnCorrectOrder()
		{
			var result = _orderRepository.GetOrderWithProducts(_orders.First().Id, _orders.First().UserId);
			result.Should().Be(_orders.First());
		}
	}
}