using Microsoft.AspNetCore.Mvc;
using OrdersManager.WebApp.Core.Models.Domain;
using OrdersManager.WebApp.Core.Services;
using OrdersManager.WebApp.Persistence.Extensions;

namespace OrdersManager.WebApp.Controllers;

// Do utworzenia kontrolera przekazać serwis do obsługi zamówień oraz loggera.
// Są to zewnętrzne zależności, które będą wstrzykiwane przez kontener DI.
// W testach należy je zamockować, aby kontroler mógł być testowany w izolacji.
[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService ordersService, ILogger<OrderController> logger) : ControllerBase
{
	[HttpPost]
	public IActionResult AddProduct(Product? product)
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
		var userId = User.GetUserId();

		try
		{
			// trzeci przypadek testowy: dodajemy produkt do zamówienia, sprawdzić czy metoda wywołałą się 1 raz
			ordersService.AddProduct(userId, product);
		}
		catch (Exception exception)
		{
			// piąty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, logowanie błędu
			logger.LogError(exception.Message);

			// szósty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, zwrócenie BadRequest
			return BadRequest();
		}

		// czwarty przypadek testowy: dodajemy produkt do zamówienia, jeśli wszystko poszło dobrze, zwracamy Ok
		return Ok();
	}

	// na podstawie id zamówienia zwraca zamówienie wraz z produktami
	[HttpGet]
	public IActionResult GetOrder(int orderId)
	{
		// pierwszy przypadek testowy: jeśli orderId jest mniejsze lub równe 0, zwraca BadRequest
		if (orderId <= 0)
		{
			return BadRequest();
		}

		// zewnętrzna zależność: pobieranie id użytkownika z kontekstu tożsamości
		var userId = User.GetUserId();
		Order order;
		try
		{
			// drugi przypadek testowy: pobieranie zamówienia wraz z produktami, sprawdzić czy metoda wywołała się 1 raz
			order = ordersService.GetOrderWithProducts(orderId, userId);
		}
		catch (Exception exception)
		{
			// piąty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, logowanie błędu
			logger.LogError(exception.Message);

			// szósty przypadek testowy: jeśli wystąpił błąd podczas dodawania produktu, zwrócenie BadRequest
			return BadRequest();
		}

		// trzeci przypadek testowy: pobieranie zamówienia wraz z produktami, jeśli wszystko poszło dobrze, zwracamy Ok
		// czwarty przypadek testowy: pobieranie zamówienia wraz z produktami, jeśli wszystko poszło dobrze, zwracamy zamówienie
		return Ok(order);
	}

	[HttpPost]
	public IActionResult DeleteProduct(int orderId, int productId)
	{
		if (productId <= 0)
		{
			return BadRequest();
		}

		if (orderId <= 0)
		{
			return BadRequest();
		}

		var userId = User.GetUserId();

		try
		{
			ordersService.DeleteProduct(orderId, productId, userId);
		}
		catch (Exception exception)
		{
			logger.LogError(exception.Message);
			return BadRequest();
		}

		return Ok();
	}

	[HttpPut]
	public IActionResult UpdateProduct(Product? product)
	{
		if (product == null)
		{
			return BadRequest();
		}

		var userId = User.GetUserId();

		try
		{
			ordersService.UpdateProduct(userId, product);
		}
		catch (Exception exception)
		{
			logger.LogError(exception.Message);
			return BadRequest();
		}

		return Ok();
	}
}