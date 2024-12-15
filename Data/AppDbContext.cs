using Microsoft.EntityFrameworkCore;
using FlowerCommerceAPI.Models;

namespace FlowerCommerceAPI.Data
{
    /// <summary>
    /// Represents the application's database context for interacting with the database.
    /// Contains DbSet properties for Product, User, and WishlistItem entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the collection of products in the database.
        /// </summary>
        public DbSet<Product>? Products { get; set; }

        /// <summary>
        /// Gets or sets the collection of users in the database.
        /// </summary>
        public DbSet<User>? Users { get; set; }

        /// <summary>
        /// Gets or sets the collection of wishlist items in the database.
        /// </summary>
        public DbSet<WishlistItem>? WishlistItems { get; set; }

        /// <summary>
        /// Configures the model relationships and other settings for the database schema.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the entity types.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                /// <summary>
                /// Ensures that the email address is unique in the User entity.
                /// </summary>
                entity.HasIndex(u => u.Email).IsUnique();

                /// <summary>
                /// Sets the default value for the CreatedAt property to the current date and time.
                /// </summary>
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                /// <summary>
                /// Sets the Name property as required with a maximum length of 100 characters.
                /// </summary>
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);

                /// <summary>
                /// Sets the Price property to be of decimal type with precision and scale of (18,2).
                /// </summary>
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });

            // Configure WishlistItem entity
            modelBuilder.Entity<WishlistItem>()
                /// <summary>
                /// Defines a composite key using UserId and ProductId for the WishlistItem entity.
                /// </summary>
                .HasKey(w => new { w.UserId, w.ProductId });

            modelBuilder.Entity<WishlistItem>()
                /// <summary>
                /// Sets up a one-to-many relationship between WishlistItem and User.
                /// </summary>
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlist)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<WishlistItem>()
                /// <summary>
                /// Sets up a one-to-many relationship between WishlistItem and Product.
                /// </summary>
                .HasOne(w => w.Product)
                .WithMany(p => p.WishlistedBy)
                .HasForeignKey(w => w.ProductId);
        }
    }
}
