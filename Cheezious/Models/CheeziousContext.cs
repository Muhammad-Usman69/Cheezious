using Microsoft.EntityFrameworkCore;

namespace Cheezious.Models
{
    public class CheeziousContext : DbContext
    {
        public CheeziousContext(DbContextOptions options) :base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relations in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relations.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }


       public DbSet<AssignOrder> AssignOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
