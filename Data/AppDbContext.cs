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

            // // Seed data for Products and Translations
            // modelBuilder.Entity<Product>().HasData(
            //     new Product
            //     {
            //         Id = 1, // Primary key or auto-generated ID
            //         CategoryId = 1,
            //         Translations = new List<ProductTranslation>
            //         {
            //             new ProductTranslation
            //             {
            //                 Language = "en-US",
            //                 Name = "Sample Product",
            //                 Description = "This is a sample product"
            //             },
            //             new ProductTranslation
            //             {
            //                 Language = "fa-IR",
            //                 Name = "محصول نمونه",
            //                 Description = "این یک محصول نمونه است"
            //             }
            //         }
            //     }
            // );

            // modelBuilder.Entity<ProductTranslation>().HasData(
            //     new ProductTranslation
            //     {
            //         ProductId = 1,
            //         Language = "en-US",
            //         Name = "Sample Product",
            //         Description = "This is a sample product"
            //     },
            //     new ProductTranslation
            //     {
            //         ProductId = 1,
            //         Language = "fa-IR",
            //         Name = "محصول نمونه",
            //         Description = "این یک محصول نمونه است"
            //     }
            // );

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
