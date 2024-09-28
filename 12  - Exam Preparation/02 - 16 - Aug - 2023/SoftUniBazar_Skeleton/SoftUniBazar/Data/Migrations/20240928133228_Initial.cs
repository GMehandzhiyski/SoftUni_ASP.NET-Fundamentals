using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniBazar.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Ad class Primary Key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Name"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Discrioption"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "OwnerId"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "ImageUrl"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "CreatedOn"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "CategoryId")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ad_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ad_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdBuyer",
                columns: table => new
                {
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "BuyerId"),
                    AdId = table.Column<int>(type: "int", nullable: false, comment: "AsId")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdBuyer", x => new { x.BuyerId, x.AdId });
                    table.ForeignKey(
                        name: "FK_AdBuyer_Ad_AdId",
                        column: x => x.AdId,
                        principalTable: "Ad",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdBuyer_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Books" },
                    { 2, "Cars" },
                    { 3, "Clothes" },
                    { 4, "Home" },
                    { 5, "Technology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_CategoryId",
                table: "Ad",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_OwnerId",
                table: "Ad",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdBuyer_AdId",
                table: "AdBuyer",
                column: "AdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdBuyer");

            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
