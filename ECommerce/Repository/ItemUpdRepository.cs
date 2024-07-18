using ECommerce.Models;
using ECommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository
{
    public class ItemUpdRepository: ItemRepository<Items>,IItemUpdateRepository
    {
        private readonly Data.ApplicationDbContext _db;
        public ItemUpdRepository(Data.ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Items> UpdateAsync(Items entity)
        {
            
            _db.Item.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
        public async Task<Items> GetItemByIdAsync(int itemId)
        {
            return await _db.Item.FindAsync(itemId);
        }

    }
}
