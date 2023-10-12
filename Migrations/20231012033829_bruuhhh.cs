using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class bruuhhh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspeosStock");

            migrationBuilder.DropTable(
                name: "EpsonStock");

            migrationBuilder.DropTable(
                name: "FlatStock");

            migrationBuilder.DropTable(
                name: "LaminationStock");

            migrationBuilder.CreateTable(
                name: "BindingCoilsStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BindingCoilType = table.Column<int>(type: "int", nullable: false),
                    RingsRatio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SheetsHeld = table.Column<int>(type: "int", nullable: false),
                    CoilSize = table.Column<double>(type: "float", nullable: false),
                    CoilsPerPack = table.Column<double>(type: "float", nullable: false),
                    PricePerPack = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BindingCoilsStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BindingCoverStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockType = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SheetsPerPack = table.Column<double>(type: "float", nullable: false),
                    PricePerPack = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BindingCoverStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SheetsPerPack = table.Column<double>(type: "float", nullable: false),
                    PricePer1000 = table.Column<double>(type: "float", nullable: false),
                    HeightOf100Sheets = table.Column<double>(type: "float", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    StockType = table.Column<int>(type: "int", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sra3AndBookletsStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SheetsPerPack = table.Column<double>(type: "float", nullable: false),
                    PricePer1000 = table.Column<double>(type: "float", nullable: false),
                    HeightOf100Sheets = table.Column<double>(type: "float", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    StockType = table.Column<int>(type: "int", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sra3AndBookletsStock", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BindingCoilsStock");

            migrationBuilder.DropTable(
                name: "BindingCoverStock");

            migrationBuilder.DropTable(
                name: "DocumentsStock");

            migrationBuilder.DropTable(
                name: "Sra3AndBookletsStock");

            migrationBuilder.CreateTable(
                name: "AspeosStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackCost = table.Column<double>(type: "float", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SheetsPerPack = table.Column<double>(type: "float", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspeosStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EpsonStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RollCost = table.Column<double>(type: "float", nullable: false),
                    RollsLength = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpsonStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackCost = table.Column<double>(type: "float", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SheetsPerPack = table.Column<double>(type: "float", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaminationStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RollCost = table.Column<double>(type: "float", nullable: false),
                    RollsLength = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaminationStock", x => x.Id);
                });
        }
    }
}
