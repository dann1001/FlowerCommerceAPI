using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerCommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceFieldsToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "TemperatureRequirementDetailsEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TemperatureRequirementDetailsPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TemperatureRequirementEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TemperatureRequirementPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemperatureRequirementRecommendationEn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemperatureRequirementRecommendationPe",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterRequirementDetailsEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterRequirementDetailsPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterRequirementEn",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterRequirementPe",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterRequirementRecommendationEn",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterRequirementRecommendationPe",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "TemperatureRequirementDetailsEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TemperatureRequirementDetailsPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TemperatureRequirementEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TemperatureRequirementPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TemperatureRequirementRecommendationEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TemperatureRequirementRecommendationPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WaterRequirementDetailsEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WaterRequirementDetailsPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WaterRequirementEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WaterRequirementPe",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WaterRequirementRecommendationEn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WaterRequirementRecommendationPe",
                table: "Products");
        }
    }
}
