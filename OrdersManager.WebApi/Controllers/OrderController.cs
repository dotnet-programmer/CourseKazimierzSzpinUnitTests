using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using NLog;
using OrdersManager.Core.Models.Domain;
using OrdersManager.Core.Services;

namespace OrdersManager.Controllers
{
	public class OrderController : ApiController
	{
		private readonly IOrderService _orderService;
		private readonly ILogger _logger;

		// Do utworzenia kontrolera przekazać serwis do obsługi zamówień oraz loggera.
		// Są to zewnętrzne zależności, które będą wstrzykiwane przez kontener DI.
		// W testach należy je zamockować, aby kontroler mógł być testowany w izolacji.
		public OrderController(IOrderService ordersService, ILogger logger)
		{
			_orderService = ordersService;
			_logger = logger;
		}

		[HttpPost]
		public IHttpActionResult AddProduct(Product product)
		{
			// pierwszy przypadek testowy: jeśli produkt jest null, zwracamy BadRequest
			if (product == null)
			{
				return BadRequest();
			}

			// drugi przypadek testowy: jeśli OrderId jest mniejsze lub równe 0, zwracamy BadRequest
			if (product.OrderId <= 0)
			{
				return BadRequest();
			}

			// zewnętrzna zależność: pobieranie id użytkownika z kontekstu tożsamości
			var userId = User.Identity.GetUserId();

			try
			{
				// trzeci przypadek testowy: dodajemy produkt do zamówienia, sprawdzić czy metoda wywołałą się 1 raz
				_orderService.AddProduct(userId, product);
			}
			catch (Exception exception)
			{
				// piąty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, logowanie błędu
				_logger.Error(exception.Message);

				// szósty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, zwrócenie BadRequest
				return BadRequest();
			}

			// czwarty przypadek testowy: dodajemy produkt do zamówienia, jeśli wszystko poszło dobrze, zwracamy Ok
			return Ok();
		}

		// na podstawie id zamówienia zwraca zamówienie wraz z produktami
		[HttpGet]
		public IHttpActionResult GetOrder(int orderId)
		{
			// pierwszy przypadek testowy: jeśli orderId jest mniejsze lub równe 0, zwraca BadRequest
			if (orderId <= 0)
			{
				return BadRequest();
			}

			// zewnętrzna zależność: pobieranie id użytkownika z kontekstu tożsamości
			var userId = User.Identity.GetUserId();
			Order order;
			try
			{
				// drugi przypadek testowy: pobieranie zamówienia wraz z produktami, sprawdzić czy metoda wywołała się 1 raz
				order = _orderService.GetOrderWithProducts(orderId, userId);
			}
			catch (Exception exception)
			{
				// piąty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, logowanie błędu
				_logger.Error(exception.Message);

				// szósty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, zwrócenie BadRequest
				return BadRequest();
			}

			// trzeci przypadek testowy: pobieranie zamówienia wraz z produktami, jeśli wszystko poszło dobrze, zwracamy Ok
			// czwarty przypadek testowy: pobieranie zamówienia wraz z produktami, jeśli wszystko poszło dobrze, zwracamy zamówienie
			return Ok(order);
		}
	}
}