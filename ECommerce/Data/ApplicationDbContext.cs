using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        
        public DbSet<Items> Item { get; set; }
        public DbSet<Order> Order { get; set; }
       
        public DbSet<User> Users {  get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Items>().HasData(
                new Items()
                {
                    Id = 1,
                    Name = "Eraser",
                    Description = "Apsara Brand",
                    Price = 5.0,
                    Quantity = 100
                    



                });
        }

    }
}
