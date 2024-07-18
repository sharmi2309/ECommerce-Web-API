using ECommerce.Models;
using ECommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository
{
    public class Repositoryorder : OrderRepository<Order>,IRepositoryorder
    {
        public Repositoryorder(Data.ApplicationDbContext context) : base(context) { }


        //public async Task<IEnumerable<Order>> GetOrdersAsync()
        //{
        //    return await _context.Order.ToListAsync();
        //}
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Order
                                 .Include(o => o.Item)   
                                 .Include(o => o.User)  
                                 .ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                var item = await _context.Item.FindAsync(order.Id);

                if (item == null)
                {
                    throw new KeyNotFoundException($"Item with ID {order.Id} not found.");
                }
                if (item.Quantity < order.Quantity || !item.Name.Equals(order.Name, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Insufficient quantity or mismatched item name.");
                }
                item.Quantity -= order.Quantity;
                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in AddOrderAsync: {ex}");
                throw;
            }
        }
      



        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            var item = await _context.Item.FindAsync(order.Id);
            if (item != null)
            {
                item.Quantity += order.Quantity;
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }
        
    }
}
