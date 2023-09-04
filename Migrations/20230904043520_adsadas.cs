using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class adsadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspeosStock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspeosStock");
        }
    }
}
