using ECommerce.Models;

namespace ECommerce.Repository.IRepository
{
        public interface IItemUpdateRepository : IItemRepository<Items>
        {
            Task<Items> UpdateAsync(Items entity);
            Task<Items> GetItemByIdAsync(int itemId);

    }
    

}
