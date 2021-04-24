using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreData.DbModels;

namespace NetCoreData.DataContext
{
    public class NetCoreDbContext : IdentityDbContext
    {
        public NetCoreDbContext(DbContextOptions options) :
            base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderItem> OrderItems
        {
            get; set;



        }
    }
}


