using Microsoft.EntityFrameworkCore;
using FlowerCommerceAPI.Models;
using System.Collections.Generic;

namespace FlowerCommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");

                // Configure relationship between Product and ProductTranslations
                entity.HasMany(p => p.Translations)
                      .WithOne()
                      .HasForeignKey(t => t.ProductId);
            });

            // Configure ProductTranslation entity (assuming this is where you store translations)
            modelBuilder.Entity<ProductTranslation>(entity =>
            {
                entity.Property(t => t.Language).IsRequired().HasMaxLength(10);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Description).HasMaxLength(1000);

                // Additional configurations for the ProductTranslation entity can go here if needed
            });

            // Configure WishlistItem entity
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
