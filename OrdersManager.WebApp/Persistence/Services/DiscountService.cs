using OrdersManager.WebApp.Core;
using OrdersManager.WebApp.Core.Services;

namespace OrdersManager.WebApp.Persistence.Services;

public class DiscountService(IUnitOfWork unitOfWork) : IDiscountService
{
	public decimal GetDiscount(int orderId, string userId)
	{
		var order = unitOfWork.Order.GetOrderWithProducts(orderId, userId);
		var user = unitOfWork.User.GetUser(userId);

		if (user.IsNewUser)
		{
			return 0;
		}

		if (order.Products == null || order.Products.Count == 0)
		{
			throw new Exception("Order doesn't contain products.");
		}

		if (order.Products.Sum(x => x.Price * x.Quantity) > 300)
		{
			return 100;
		}

		return 10;
	}
}