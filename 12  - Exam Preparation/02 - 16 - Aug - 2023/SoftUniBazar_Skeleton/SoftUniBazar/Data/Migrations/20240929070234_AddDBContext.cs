using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniBazar.Data.Migrations
{
    public partial class AddDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_AspNetUsers_OwnerId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_AdBuyer_Ad_AdId",
                table: "AdBuyer");

            migrationBuilder.DropForeignKey(
                name: "FK_AdBuyer_AspNetUsers_BuyerId",
                table: "AdBuyer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdBuyer",
                table: "AdBuyer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ad",
                table: "Ad");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "AdBuyer",
                newName: "AdBuyers");

            migrationBuilder.RenameTable(
                name: "Ad",
                newName: "Ads");

            migrationBuilder.RenameIndex(
                name: "IX_AdBuyer_AdId",
                table: "AdBuyers",
                newName: "IX_AdBuyers_AdId");

            migrationBuilder.RenameIndex(
                name: "IX_Ad_OwnerId",
                table: "Ads",
                newName: "IX_Ads_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Ad_CategoryId",
                table: "Ads",
                newName: "IX_Ads_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdBuyers",
                table: "AdBuyers",
                columns: new[] { "BuyerId", "AdId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ads",
                table: "Ads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdBuyers_Ads_AdId",
                table: "AdBuyers",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdBuyers_AspNetUsers_BuyerId",
                table: "AdBuyers",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_OwnerId",
                table: "Ads",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Categories_CategoryId",
                table: "Ads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdBuyers_Ads_AdId",
                table: "AdBuyers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdBuyers_AspNetUsers_BuyerId",
                table: "AdBuyers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_OwnerId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Categories_CategoryId",
                table: "Ads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ads",
                table: "Ads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdBuyers",
                table: "AdBuyers");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Ads",
                newName: "Ad");

            migrationBuilder.RenameTable(
                name: "AdBuyers",
                newName: "AdBuyer");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_OwnerId",
                table: "Ad",
                newName: "IX_Ad_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_CategoryId",
                table: "Ad",
                newName: "IX_Ad_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AdBuyers_AdId",
                table: "AdBuyer",
                newName: "IX_AdBuyer_AdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ad",
                table: "Ad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdBuyer",
                table: "AdBuyer",
                columns: new[] { "BuyerId", "AdId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_AspNetUsers_OwnerId",
                table: "Ad",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdBuyer_Ad_AdId",
                table: "AdBuyer",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdBuyer_AspNetUsers_BuyerId",
                table: "AdBuyer",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
