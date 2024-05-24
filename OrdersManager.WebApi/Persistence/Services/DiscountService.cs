using OrdersManager.Core;
using OrdersManager.Core.Services;
using System;
using System.Linq;

namespace OrdersManager.Persistence.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public decimal GetDiscount(int orderId, string userId)
        {
            var order = _unitOfWork.Order.GetOrderWithProducts(orderId, userId);
            var user = _unitOfWork.User.GetUser(userId);

            if (user.IsNewUser)
                return 0;

            if (order.Products == null || !order.Products.Any())
                throw new Exception("Order doesn't contain products.");

            if (order.Products.Sum(x => x.Price * x.Quantity) > 300)
                return 100;

            return 10;
        }
    }
}