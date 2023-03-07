using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();

                entity.HasIndex(e => e.CategoryName).IsUnique(true);
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();

                entity.HasOne(e => e.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(e => e.CategoryId);

                entity.HasIndex(e => e.ProductName).IsUnique(true);
            });

            builder.Entity<Product>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,4)");

            builder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();

                entity.HasOne(e => e.Customer)
                .WithMany(user => user.Addresses)
                .HasForeignKey(e => e.CustomerId);
            });

            builder.Entity<CartItem>().HasKey(p => new { p.ProductId, p.CustomerId });

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();

                entity.HasOne(e => e.Customer)
                .WithMany(user => user.Orders)
                .HasForeignKey(e => e.CustomerId);
            });

            builder.Entity<OrderDetail>()
                .HasKey(p => new { p.OrderId, p.ProductId });


            builder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,4)");

            builder.Entity<OrderDetail>()
                .Property(e => e.Total)
                .HasColumnType("decimal(18,4)");

            SeedRoles(builder);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole() { Name = "SuperAdmin", ConcurrencyStamp = "0", NormalizedName = "SuperAdmin" },
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                    new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
    }
}
