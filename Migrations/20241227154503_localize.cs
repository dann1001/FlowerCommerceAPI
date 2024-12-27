using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerCommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class localize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LightRequirementDetailsEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LightRequirementDetailsPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LightRequirementEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LightRequirementPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LightRequirementRecommendationEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LightRequirementRecommendationPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NamePe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PricePe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoilRequirementDetailsEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoilRequirementDetailsPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoilRequirementEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoilRequirementPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoilRequirementRecommendationEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoilRequirementRecommendationPe",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementRecommendationPe",
                table: "Products",
                newName: "WaterRequirementRecommendation");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementRecommendationEn",
                table: "Products",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementPe",
                table: "Products",
                newName: "WaterRequirementDetails");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementEn",
                table: "Products",
                newName: "WaterRequirement");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementDetailsPe",
                table: "Products",
                newName: "TemperatureRequirementDetails");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementDetailsEn",
                table: "Products",
                newName: "TemperatureRequirement");

            migrationBuilder.RenameColumn(
                name: "TypePe",
                table: "Products",
                newName: "TemperatureRequirementRecommendation");

            migrationBuilder.RenameColumn(
                name: "TypeEn",
                table: "Products",
                newName: "SoilRequirementRecommendation");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementRecommendationPe",
                table: "Products",
                newName: "LightRequirementRecommendation");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementRecommendationEn",
                table: "Products",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementPe",
                table: "Products",
                newName: "SoilRequirementDetails");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementEn",
                table: "Products",
                newName: "SoilRequirement");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementDetailsPe",
                table: "Products",
                newName: "LightRequirementDetails");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementDetailsEn",
                table: "Products",
                newName: "LightRequirement");

            migrationBuilder.CreateTable(
                name: "ProductTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTranslations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_ProductId",
                table: "ProductTranslations",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTranslations");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementRecommendation",
                table: "Products",
                newName: "WaterRequirementRecommendationPe");

            migrationBuilder.RenameColumn(
                name: "WaterRequirementDetails",
                table: "Products",
                newName: "WaterRequirementPe");

            migrationBuilder.RenameColumn(
                name: "WaterRequirement",
                table: "Products",
                newName: "WaterRequirementEn");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Products",
                newName: "WaterRequirementRecommendationEn");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementRecommendation",
                table: "Products",
                newName: "TypePe");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirementDetails",
                table: "Products",
                newName: "WaterRequirementDetailsPe");

            migrationBuilder.RenameColumn(
                name: "TemperatureRequirement",
                table: "Products",
                newName: "WaterRequirementDetailsEn");

            migrationBuilder.RenameColumn(
                name: "SoilRequirementRecommendation",
                table: "Products",
                newName: "TypeEn");

            migrationBuilder.RenameColumn(
                name: "SoilRequirementDetails",
                table: "Products",
                newName: "TemperatureRequirementPe");

            migrationBuilder.RenameColumn(
                name: "SoilRequirement",
                table: "Products",
                newName: "TemperatureRequirementEn");

            migrationBuilder.RenameColumn(
                name: "LightRequirementRecommendation",
                table: "Products",
                newName: "TemperatureRequirementRecommendationPe");

            migrationBuilder.RenameColumn(
                name: "LightRequirementDetails",
                table: "Products",
                newName: "TemperatureRequirementDetailsPe");

            migrationBuilder.RenameColumn(
                name: "LightRequirement",
                table: "Products",
                newName: "TemperatureRequirementDetailsEn");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "TemperatureRequirementRecommendationEn");

            migrationBuilder.AddColumn<string>(
                name: "CategoryEn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryPe",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LightRequirementDetailsEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LightRequirementDetailsPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LightRequirementEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LightRequirementPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LightRequirementRecommendationEn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LightRequirementRecommendationPe",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NamePe",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoilRequirementDetailsEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoilRequirementDetailsPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoilRequirementEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoilRequirementPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoilRequirementRecommendationEn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoilRequirementRecommendationPe",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
