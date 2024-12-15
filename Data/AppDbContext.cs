using Microsoft.EntityFrameworkCore;
using FlowerCommerceAPI.Models;

namespace FlowerCommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Register your models here
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Add additional configurations here if needed
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique(); // Ensure unique email addresses
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<WishlistItem>()
    .HasKey(w => new { w.UserId, w.ProductId });

modelBuilder.Entity<WishlistItem>()
    .HasOne(w => w.User)
    .WithMany(u => u.Wishlist)
    .HasForeignKey(w => w.UserId);

modelBuilder.Entity<WishlistItem>()
    .HasOne(w => w.Product)
    .WithMany(p => p.WishlistedBy)
    .HasForeignKey(w => w.ProductId);
        }
    }
}
