using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class flat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlatFlatSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeMultiplier = table.Column<int>(type: "int", nullable: true),
                    LaminationCost = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatFlatSizes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatFlatSizes");
        }
    }
}
