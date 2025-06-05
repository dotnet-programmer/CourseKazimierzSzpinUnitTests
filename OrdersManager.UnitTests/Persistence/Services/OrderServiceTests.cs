using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Core.Services;
using OrdersManager.Persistence.Services;

namespace OrdersManager.UnitTests.Persistence.Services
{
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
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockDiscountService = new Mock<IDiscountService>();
			_orderService = new OrderService(_mockUnitOfWork.Object, _mockDiscountService.Object);

			_userId = "1";
			_product = new Product
			{
				Id = 1,
				Name = "Test Product",
				Price = 100,
				OrderId = 1
			};
			_order = new Order
			{
				Id = 1,
				UserId = _userId,
				TotalPrice = 0,
				Products = new List<Product>()
			};

			_mockUnitOfWork
				.Setup(x => x.Order.GetOrderWithProducts(_product.OrderId, _userId))
				.Returns(_order);
		}

		#region AddProduct Tests

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

		#endregion AddProduct Tests

		#region GetOrderWithProducts Tests

		[Test]
		public void GetOrderWithProducts_WhenCalled_ShouldReturnOrder()
		{
			Order order = _orderService.GetOrderWithProducts(_order.Id, _userId);
			order.Should().BeSameAs(_order);
		}

		[Test]
		public void GetOrderWithProducts_WhenOrderIsNull_ShouldThrowAnException()
		{
			_mockUnitOfWork
				.Setup(x => x.Order.GetOrderWithProducts(_order.Id, _userId))
				.Throws(new Exception("Order doesn't exists."));
			Action action = () => _orderService.GetOrderWithProducts(_order.Id, _userId);
			action.Should().Throw<Exception>().WithMessage("Order doesn't exists.");
		}

		#endregion GetOrderWithProducts Tests
	}
}