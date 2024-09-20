using ChromeRigs.Entities.Components;
using ChromeRigs.Entities.Customers;
using ChromeRigs.Entities.Orders;
using ChromeRigs.Entities.PCs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChromeRigs.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Component> Components { get; set; }
        public DbSet<PC> PCs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
