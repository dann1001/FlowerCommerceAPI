using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerCommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddWishlistItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Wishlist",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wishlist",
                table: "Users");
        }
    }
}
