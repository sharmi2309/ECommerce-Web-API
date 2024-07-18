using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Models.DTO;
using ECommerce.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.Passwordsalt))
            {
                return null;
            }
            return user;
        }
        public async Task<int?> GetUserIdByUsernameAsync(string username)
        {
            try
            {
                
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

                if (user == null)
                {
                    Console.WriteLine($"User '{username}' not found in database.");
                    return null;
                }

                return user.UserId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetUserIdByUsernameAsync: {ex}");
                throw;
            }
        }
        [HttpGet("User/{username}")]
        //public async Task<List<Order>> GetOrdersByUsernameAsync(string username)
        //{
        //    var orders = await _dbContext.Order
        //        .Include(o => o.Item)
        //        .Where(o => o.User.Username == username)
        //        .ToListAsync();
        //    if (orders == null)
        //    {
        //        throw new KeyNotFoundException($"User '{username}' not found.");
        //    }

        //    return orders;
        //}



        public async Task<List<Order>> GetOrdersByUsernameAsync(string username)
        {
            var normalizedUsername = username.Trim().ToLower();
            var user = await _dbContext.Users.Include(u => u.Order).ThenInclude(o => o.Item).FirstOrDefaultAsync(u => u.Username.ToLower() == normalizedUsername);
            if (user == null)
            {
                throw new KeyNotFoundException($"User '{username}' not found.");
            }

            var orders = user.Order.ToList();
            return orders;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }

        public async Task AddUser(User user)
        {
            _dbContext.Users.Add(user); 
            await _dbContext.SaveChangesAsync();
        }
    }
}


