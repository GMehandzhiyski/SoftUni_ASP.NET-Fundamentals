using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class AddDeleteAtributs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarParticipants_AspNetUsers_ParticipantId",
                table: "SeminarParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarParticipants_AspNetUsers_ParticipantId",
                table: "SeminarParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarParticipants_AspNetUsers_ParticipantId",
                table: "SeminarParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarParticipants_AspNetUsers_ParticipantId",
                table: "SeminarParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
