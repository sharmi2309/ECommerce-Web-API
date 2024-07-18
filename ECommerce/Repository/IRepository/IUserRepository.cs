using ECommerce.Models;
using ECommerce.Models.DTO;
using System.Security.Claims;

namespace ECommerce.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAndPassword(string username, string password);
        Task<int?> GetUserIdByUsernameAsync(string username);
       
        Task<List<Order>> GetOrdersByUsernameAsync(string username);

        Task<bool> UserExists(string username);
        Task AddUser(User user);
    }
}
