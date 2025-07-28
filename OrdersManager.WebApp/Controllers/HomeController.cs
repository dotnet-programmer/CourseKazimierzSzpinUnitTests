using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.WebApp.Core.ViewModels;

namespace OrdersManager.WebApp.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
	public IActionResult Index()
		=> View();

	public IActionResult Privacy()
		=> View();

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
		=> View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}