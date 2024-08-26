using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Forum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { new Guid("004b15fe-536d-4526-808a-6d0d8d31db3b"), "The Second post will also be about performing CRUD operations in MVC. It is so mutch fun! I adore it! I love aspNetCore", "My second post" },
                    { new Guid("0164f0f3-f21c-42cf-98ec-6f4b4cbd1aee"), "My first post will be about performing CRUD operations in MVC. It is so mutch fun! I love it. I love EF CORE", "My first post" },
                    { new Guid("c4844444-7cc2-4903-8de4-98747ed9fa95"), "Hello! It is so mutch fun! My third post will be about performing CRUD operations in MVC YAY. It is so mutch fun!", "My third" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
