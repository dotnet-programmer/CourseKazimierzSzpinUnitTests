using OrdersManager.Core;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Core.Repositories;
using System;
using System.Linq;
using System.Data.Entity;

namespace OrdersManager.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IApplicationDbContext _context;

        public OrderRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public Order GetOrderWithProducts(int orderId, string userId)
        {
            var order = _context.Orders
                .Include(x => x.Products)
                .FirstOrDefault(x => x.Id == orderId && x.UserId == userId);

            if (order == null)
                throw new NullReferenceException("Order doesn't exists.");

            return order;
        }

        public void UpdateTotalPrice(int orderId, string userId, decimal discount)
        {
            var order = GetOrderWithProducts(orderId, userId);

            if (order.Products == null || !order.Products.Any())
                order.TotalPrice = 0;

            var totalPrice = order.Products.Sum(x => x.Price * x.Quantity);

            var totalPriceWithDiscount = totalPrice - discount;

            order.TotalPrice = totalPriceWithDiscount;
        }
    }
}