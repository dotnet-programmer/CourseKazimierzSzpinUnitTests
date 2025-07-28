using OrdersManager.WebApp.Core;
using OrdersManager.WebApp.Core.Models.Domain;
using OrdersManager.WebApp.Core.Repositories;

namespace OrdersManager.WebApp.Persistence.Repositories;

public class UserRepository(IApplicationDbContext context) : IUserRepository
{
	public ApplicationUser GetUser(string userId)
		=> context.Users.FirstOrDefault(x => x.Id == userId) ?? throw new NullReferenceException("User doesn't exists.");

	//public ApplicationUser GetUser(string userId)
	//{
	//	var user = context.Users.FirstOrDefault(x => x.Id == userId);
	//	if (user == null)
	//	{
	//		throw new NullReferenceException("User doesn't exists.");
	//	}
	//	return user;
	//}
}