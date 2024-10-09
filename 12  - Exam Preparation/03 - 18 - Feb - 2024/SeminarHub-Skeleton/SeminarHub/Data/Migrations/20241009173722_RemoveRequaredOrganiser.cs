using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class RemoveRequaredOrganiser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Lecturer",
                table: "Seminars",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                comment: "Lecturer",
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldComment: "Lecturer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Lecturer",
                table: "Seminars",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                comment: "Lecturer",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldComment: "Lecturer");
        }
    }
}
