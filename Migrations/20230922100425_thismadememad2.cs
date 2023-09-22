using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class thismadememad2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMultiplier",
                table: "FlatSizes");

            migrationBuilder.DropColumn(
                name: "TestQuantity",
                table: "FlatSizes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TestMultiplier",
                table: "FlatSizes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TestQuantity",
                table: "FlatSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
