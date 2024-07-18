using ECommerce.Models;

namespace ECommerce.Repository.IRepository
{
    public interface IOrderRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
    }
}
