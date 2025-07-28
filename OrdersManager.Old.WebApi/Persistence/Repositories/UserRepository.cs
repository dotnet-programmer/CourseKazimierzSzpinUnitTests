using System;
using System.Linq;
using OrdersManager.Core;
using OrdersManager.Core.Repositories;
using OrdersManager.Models;

namespace OrdersManager.Persistence.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IApplicationDbContext _context;

		public UserRepository(IApplicationDbContext context)
			=> _context = context;

		public ApplicationUser GetUser(string userId)
			=> _context.Users.FirstOrDefault(x => x.Id == userId) ?? throw new NullReferenceException("User doesn't exists.");

		//public ApplicationUser GetUser(string userId)
		//{
		//	var user = _context.Users.FirstOrDefault(x => x.Id == userId);
		//	if (user == null)
		//	{
		//		throw new NullReferenceException("User doesn't exists.");
		//	}
		//	return user;
		//}
	}
}