using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class heehwerjfneswdikf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "FlatFlatSizes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "FlatFlatSizes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "EpsonFlatSizes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "EpsonFlatSizes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "BookletFlatSizes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "BookletFlatSizes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "AspeosFlatSizes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "AspeosFlatSizes",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "FlatFlatSizes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "FlatFlatSizes");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "EpsonFlatSizes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "EpsonFlatSizes");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "BookletFlatSizes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "BookletFlatSizes");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspeosFlatSizes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "AspeosFlatSizes");
        }
    }
}
