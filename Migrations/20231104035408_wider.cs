using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class wider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WideFormatFormInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomFlatSize = table.Column<bool>(type: "bit", nullable: false),
                    Kinds = table.Column<double>(type: "float", nullable: false),
                    Pages = table.Column<double>(type: "float", nullable: false),
                    FlatSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Colour = table.Column<bool>(type: "bit", nullable: false),
                    Folds = table.Column<int>(type: "int", nullable: false),
                    Creases = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    FileHandlingCost = table.Column<double>(type: "float", nullable: false),
                    DesignCost = table.Column<double>(type: "float", nullable: false),
                    SetupCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WideFormatFormInputs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WideFormatFormInputs");
        }
    }
}
