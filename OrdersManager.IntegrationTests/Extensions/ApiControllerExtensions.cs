using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrdersManager.IntegrationTests.Extensions;

public static class ApiControllerExtensions
{
	// do usunięcia zależności od użytkownika - mock Usera
	public static void MockCurrentUser(this ControllerBase controller, string userId, string userName)
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.Name, userName),
			new Claim(ClaimTypes.NameIdentifier, userId)
		};
		var identity = new ClaimsIdentity(claims, "TestAuthType");
		var principal = new ClaimsPrincipal(identity);

		controller.ControllerContext = new ControllerContext
		{
			HttpContext = new DefaultHttpContext { User = principal }
		};
	}
}