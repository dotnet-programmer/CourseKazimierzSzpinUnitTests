using System.Linq;
using NUnit.Framework;
using OrdersManager.Models;

namespace OrdersManager.IntegrationTests.Helpers
{
	[SetUpFixture]
	public class SetUpFixture
	{
		[OneTimeSetUp]
		public void OneTimeSetUp()
			=> AddUserIfDoesntExists();

		private void AddUserIfDoesntExists()
		{
			using (var context = new ApplicationDbContext())
			{
				if (context.Users.Any())
				{
					return;
				}

				context.Users.Add(new ApplicationUser { UserName = "1", Email = "2", PasswordHash = "3" });
				context.SaveChanges();
			}
		}
	}
}