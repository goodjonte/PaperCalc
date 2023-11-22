using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FlatSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForCalculation = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    QuantityMin = table.Column<int>(type: "int", nullable: false),
                    QuantityMax = table.Column<int>(type: "int", nullable: false),
                    MultiplierMin = table.Column<double>(type: "float", nullable: false),
                    MultiplierMax = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatSizes", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "WideFormatStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RollWidth = table.Column<double>(type: "float", nullable: false),
                    RollPrice = table.Column<double>(type: "float", nullable: false),
                    RollLength = table.Column<double>(type: "float", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    StockType = table.Column<int>(type: "int", nullable: false),
                    CoatType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WideFormatStock", x => x.Id);
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
                name: "BookletFormInputs");

            migrationBuilder.DropTable(
                name: "DocumentFormInputs");

            migrationBuilder.DropTable(
                name: "DocumentsStock");

            migrationBuilder.DropTable(
                name: "FlatSizes");

            migrationBuilder.DropTable(
                name: "InputsForJobs");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Sra3AndBookletsStock");

            migrationBuilder.DropTable(
                name: "Sra3FormInput");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WideFormatFormInputs");

            migrationBuilder.DropTable(
                name: "WideFormatStock");
        }
    }
}
