using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class TestTaskContext : DbContext
    {
        public TestTaskContext(DbContextOptions<TestTaskContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "HDD 1TB", Quantity = 55, Price = 74.09m },
                new Product { Id = 2, Title = "HDD SSD 512GB", Quantity = 102, Price = 190.99m },
                new Product { Id = 3, Title = "RAM DDR4 16GB", Quantity = 47, Price = 80.32m }
            );
        }
    }


}
