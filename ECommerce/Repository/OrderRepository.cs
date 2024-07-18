using ECommerce.Models;
using ECommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Repository
{   
        public class OrderRepository<T> : IOrderRepository<T> where T : class
        {
            protected readonly Data.ApplicationDbContext _context;
            protected readonly DbSet<T> _dbSet;

            public OrderRepository(Data.ApplicationDbContext context)
            {
                _context = context;
                _dbSet = _context.Set<T>();
            }



        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }       
            public async Task DeleteAsync(int id)
            {
                T entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }
}
