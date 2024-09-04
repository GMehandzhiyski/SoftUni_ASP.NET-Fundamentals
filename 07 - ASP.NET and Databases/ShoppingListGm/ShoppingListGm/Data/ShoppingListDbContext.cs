using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingListGm.Data.Models;

namespace ShoppingListGm.Data
{
	public class ShoppingListDbContext : DbContext
	{

        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options)
            :base (options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasData(
				new Product() { Id = 1, ProductName = "Cheese" },
				new Product() { Id = 2, ProductName = "Milk" });
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Product> Products { get; set; } = null!;

	}
}
