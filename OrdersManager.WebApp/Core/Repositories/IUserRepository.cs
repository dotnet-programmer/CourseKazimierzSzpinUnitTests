using OrdersManager.WebApp.Core.Models.Domain;

namespace OrdersManager.WebApp.Core.Repositories;

public interface IUserRepository
{
	ApplicationUser GetUser(string userId);
}