namespace OrdersManager.WebApp.Core.Services;

public interface IDiscountService
{
	decimal GetDiscount(int orderId, string userId);
}