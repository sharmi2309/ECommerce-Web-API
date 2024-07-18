using ECommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Repository
{
    public class ItemRepository<T> : IItemRepository<T> where T : class
    {
        private readonly Data.ApplicationDbContext _dbdata;
        internal DbSet<T> dbset;

        public ItemRepository(Data.ApplicationDbContext dbdata)
        {
            _dbdata = dbdata;
            this.dbset = _dbdata.Set<T>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();

        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbset;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            return await query.FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await saveAsync();
        }
        public async Task RemoveAsync(T entity)
        {
            dbset.Remove(entity);
            await saveAsync();
        }
        public async Task saveAsync()
        {
            await _dbdata.SaveChangesAsync();
        }


    }
}
        