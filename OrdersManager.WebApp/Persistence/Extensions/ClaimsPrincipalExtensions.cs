using System.Security.Claims;

namespace OrdersManager.WebApp.Persistence.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static string GetUserId(this ClaimsPrincipal model)
	{
		var name = model.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new NullReferenceException("User not found!");
		}
		return name;
	}
}