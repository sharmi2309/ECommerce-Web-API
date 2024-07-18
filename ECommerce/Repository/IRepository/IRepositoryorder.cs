using ECommerce.Models;
using System.Linq.Expressions;

namespace ECommerce.Repository.IRepository
{
    public interface IRepositoryorder : IOrderRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        //Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);


        Task AddOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
