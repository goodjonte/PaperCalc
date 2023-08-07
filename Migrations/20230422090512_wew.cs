using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperCalc.Migrations
{
    /// <inheritdoc />
    public partial class wew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GSM");

            migrationBuilder.DropColumn(
                name: "gsmJsonArray",
                table: "Paper");

            migrationBuilder.AddColumn<double>(
                name: "PackCost",
                table: "Paper",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PackQuantity",
                table: "Paper",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SizeId",
                table: "Paper",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackCost",
                table: "Paper");

            migrationBuilder.DropColumn(
                name: "PackQuantity",
                table: "Paper");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Paper");

            migrationBuilder.AddColumn<string>(
                name: "gsmJsonArray",
                table: "Paper",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GSM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GSM_Paper_PaperId",
                        column: x => x.PaperId,
                        principalTable: "Paper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GSM_PaperId",
                table: "GSM",
                column: "PaperId");
        }
    }
}
