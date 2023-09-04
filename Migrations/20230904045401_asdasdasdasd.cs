using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class asdasdasdasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspeosStock");

            migrationBuilder.CreateTable(
                name: "FlatStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackCost = table.Column<double>(type: "float", nullable: false),
                    SheetsPerPack = table.Column<double>(type: "float", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatStock", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatStock");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspeosStock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
