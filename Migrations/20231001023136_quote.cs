using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class quote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuissnessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Turnaround = table.Column<double>(type: "float", nullable: true),
                    DeliveryCost = table.Column<double>(type: "float", nullable: true),
                    DesignCost = table.Column<double>(type: "float", nullable: true),
                    SetupCost = table.Column<double>(type: "float", nullable: true),
                    JobTypeForDTO = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    FlatSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FlatSizeHW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoatType = table.Column<int>(type: "int", nullable: true),
                    CustomSize = table.Column<bool>(type: "bit", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrintedSides = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfHolePunches = table.Column<int>(type: "int", nullable: false),
                    Trimming = table.Column<bool>(type: "bit", nullable: false),
                    Lamination = table.Column<bool>(type: "bit", nullable: false),
                    SmallJob = table.Column<bool>(type: "bit", nullable: false),
                    Urgent = table.Column<bool>(type: "bit", nullable: false),
                    FileHandlingFee = table.Column<double>(type: "float", nullable: true),
                    Creasing = table.Column<int>(type: "int", nullable: false),
                    Folds = table.Column<int>(type: "int", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: true),
                    CopyQuantity = table.Column<int>(type: "int", nullable: true),
                    Binding = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quote");
        }
    }
}
