using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class jobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookletFormInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kinds = table.Column<double>(type: "float", nullable: false),
                    Pages = table.Column<double>(type: "float", nullable: false),
                    FlatSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomFlatSize = table.Column<bool>(type: "bit", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    CoverStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InnerStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Colour = table.Column<bool>(type: "bit", nullable: false),
                    HolePunches = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    FileHandlingCost = table.Column<double>(type: "float", nullable: false),
                    DesignCost = table.Column<double>(type: "float", nullable: false),
                    SetupCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookletFormInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFormInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kinds = table.Column<double>(type: "float", nullable: false),
                    Pages = table.Column<double>(type: "float", nullable: false),
                    FlatSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoubleSided = table.Column<bool>(type: "bit", nullable: false),
                    Colour = table.Column<bool>(type: "bit", nullable: false),
                    Lamination = table.Column<bool>(type: "bit", nullable: false),
                    Binding = table.Column<int>(type: "int", nullable: false),
                    Folds = table.Column<int>(type: "int", nullable: false),
                    Staples = table.Column<int>(type: "int", nullable: false),
                    HolePunches = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    FileHandlingCost = table.Column<double>(type: "float", nullable: false),
                    DesignCost = table.Column<double>(type: "float", nullable: false),
                    SetupCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFormInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputsForJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputsForJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Buissnessname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sra3FormInput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kinds = table.Column<double>(type: "float", nullable: false),
                    FlatSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomFlatSize = table.Column<bool>(type: "bit", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    DoubleSided = table.Column<bool>(type: "bit", nullable: false),
                    Colour = table.Column<bool>(type: "bit", nullable: false),
                    Lamination = table.Column<bool>(type: "bit", nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Creases = table.Column<int>(type: "int", nullable: false),
                    Folds = table.Column<int>(type: "int", nullable: false),
                    Staples = table.Column<int>(type: "int", nullable: false),
                    HolePunches = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    FileHandlingCost = table.Column<double>(type: "float", nullable: false),
                    DesignCost = table.Column<double>(type: "float", nullable: false),
                    SetupCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sra3FormInput", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookletFormInputs");

            migrationBuilder.DropTable(
                name: "DocumentFormInputs");

            migrationBuilder.DropTable(
                name: "InputsForJobs");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Sra3FormInput");
        }
    }
}
