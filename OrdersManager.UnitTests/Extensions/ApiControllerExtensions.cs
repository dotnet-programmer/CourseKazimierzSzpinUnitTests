﻿using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace OrdersManager.UnitTests.Extensions
{
	public static class ApiControllerExtensions
	{
		public static void MockCurrentUser(this ApiController controller, string userId, string userName)
		{
			GenericIdentity identity = new GenericIdentity(userName);
			identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userName));
			identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));
			GenericPrincipal principal = new GenericPrincipal(identity, null);
			controller.User = principal;
		}
	}
}