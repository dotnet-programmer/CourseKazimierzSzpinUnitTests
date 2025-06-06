using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Models;
using OrdersManager.Persistence.Services;

namespace OrdersManager.UnitTests.Persistence.Services
{
	internal class DiscountServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private DiscountService _discountService;
		private ApplicationUser _user;
		private Order _order;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_discountService = new DiscountService(_mockUnitOfWork.Object);
			_user = new ApplicationUser() { Id = "1" };
			_order = new Order() { Id = 1 };

			_mockUnitOfWork
				.Setup(x => x.Order.GetOrderWithProducts(_order.Id, _user.Id))
				.Returns(_order);

			_mockUnitOfWork
				.Setup(x => x.User.GetUser(_user.Id))
				.Returns(_user);
		}

		[Test]
		public void GetDiscount_WhenUserIsNew_ShouldReturn0()
		{
			_user.IsNewUser = true;

			var discount = _discountService.GetDiscount(_order.Id, _user.Id);

			discount.Should().Be(0);
		}

		[Test]
		public void GetDiscount_WhenOrderDoesntHaveProducts_ShouldThrowAnException()
		{
			Action action = () => _discountService.GetDiscount(_order.Id, _user.Id);

			action.Should().Throw<Exception>().WithMessage("*Order doesn't contain products.*");
		}

		[Test]
		public void GetDiscount_WhenOrderTotalPriceIsGraterThan300_ShouldReturn100()
		{
			_order.Products.Add(new Product { Price = 1, Quantity = 1 });
			_order.Products.Add(new Product { Price = 100, Quantity = 3 });

			var result = _discountService.GetDiscount(_order.Id, _user.Id);

			result.Should().Be(100);
		}

		[Test]
		public void GetDiscount_WhenOrderTotalPriceIsLowerOrEqual300_ShouldReturn10()
		{
			_order.Products.Add(new Product { Price = 1, Quantity = 1 });

			var result = _discountService.GetDiscount(_order.Id, _user.Id);

			result.Should().Be(10);
		}
	}
}