using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class doom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspeosFlatSizes");

            migrationBuilder.DropTable(
                name: "BookletFlatSizes");

            migrationBuilder.DropTable(
                name: "EpsonFlatSizes");

            migrationBuilder.DropTable(
                name: "FlatFlatSizes");


            migrationBuilder.CreateTable(
                name: "FlatSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForCalculation = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    QuantityMin = table.Column<int>(type: "int", nullable: true),
                    QuantityMax = table.Column<int>(type: "int", nullable: true),
                    MultiplierMin = table.Column<double>(type: "float", nullable: true),
                    MultiplierMax = table.Column<double>(type: "float", nullable: true),
                    TestQuantity = table.Column<int>(type: "int", nullable: true),
                    TestMultiplier = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatSizes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatSizes");

            migrationBuilder.CreateTable(
                name: "AspeosFlatSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CutsPerSRA3 = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    LaminationCost = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PiecesPerSRA3 = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspeosFlatSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookletFlatSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookletFlatSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EpsonFlatSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CutsPerRoll = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RollLength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpsonFlatSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatFlatSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    LaminationCost = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeMultiplier = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatFlatSizes", x => x.Id);
                });

            
        }
    }
}
