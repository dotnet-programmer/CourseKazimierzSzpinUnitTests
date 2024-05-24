using OrdersManager.Models;

namespace OrdersManager.Core.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUser(string userId);
    }
}