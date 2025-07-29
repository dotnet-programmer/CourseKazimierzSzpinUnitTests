using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OrdersManager.WebApp.Core.Models.Domain;
using OrdersManager.WebApp.Persistence;

// żeby to zadziałało, namespace musi być taki sam, jak klasa z testami
namespace OrdersManager.IntegrationTests.Controllers;

// klasa, która będzie wywoływana przed każdym pakietem testów, czyli raz przed uruchmieniem wszystkich testów
// dodaje do bazy danych nowego użytkownika, żeby w bazie był już jakikolwiek na czas testów, żeby można było przypisać do niego np. zamówienie
[SetUpFixture]
public class SetUpFixture
{
	[OneTimeSetUp]
	public void OneTimeSetUp()
		=> AddUserIfDoesntExists();

	private void AddUserIfDoesntExists()
	{
		var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
		var config = builder.Build();

		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseSqlServer(config["ConnectionString"])
			.Options;

		using (var context = new ApplicationDbContext(options))
		{
			if (!context.Users.Any())
			{
				context.Users.Add(new ApplicationUser { UserName = "1", Email = "2", PasswordHash = "3" });
				context.SaveChanges();
			}
		}
	}
}