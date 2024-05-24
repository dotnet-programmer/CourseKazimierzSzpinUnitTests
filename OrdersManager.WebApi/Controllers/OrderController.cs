using Microsoft.AspNet.Identity;
using NLog;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Core.Services;
using System;
using System.Web.Http;

namespace OrdersManager.Controllers
{
    public class OrderController : ApiController
    {
        private IOrderService _orderService;
        private ILogger _logger;

        public OrderController(IOrderService ordersService, ILogger logger)
        {
            _orderService = ordersService;
            _logger = logger;
        }

        [HttpPost]
        public IHttpActionResult AddProduct(Product product)
        {
            if (product == null)
                return BadRequest();

            if (product.OrderId <= 0)
                return BadRequest();

            var userId = User.Identity.GetUserId();

            try
            {
                _orderService.AddProduct(userId, product);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetOrder(int orderId)
        {
            if (orderId <= 0)
                return BadRequest();

            var userId = User.Identity.GetUserId();
            Order order = null;

            try
            {
                order = _orderService.GetOrderWithProducts(orderId, userId);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
                return BadRequest();
            }

            return Ok(order);
        }
    }
}
