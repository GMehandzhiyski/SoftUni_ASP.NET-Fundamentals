using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homies.Data.Migrations
{
    public partial class SeedNewEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Description", "End", "Name", "Start", "TypeId" },
                values: new object[] { new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A conference for technology enthusiasts and professionals.", new DateTime(2024, 5, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), "Tech Conference 2024", new DateTime(2024, 5, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "Description", "End", "Name", "Start", "TypeId" },
                values: new object[] { new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "A festival celebrating the best in music.", new DateTime(2024, 6, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), "Music Festival 2024", new DateTime(2024, 6, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedOn", "Description", "End", "Name", "OrganiserId", "Start", "TypeId" },
                values: new object[] { 4, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "An event for startups to pitch their ideas to investors.", new DateTime(2024, 7, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), "Startup Pitch Event", "30c121b4-41dd-4b57-9448-243bcf213958", new DateTime(2024, 7, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Description", "End", "Name", "Start", "TypeId" },
                values: new object[] { new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "A festival celebrating the best in music.", new DateTime(2024, 6, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), "Music Festival 2024", new DateTime(2024, 6, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "Description", "End", "Name", "Start", "TypeId" },
                values: new object[] { new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "An event for startups to pitch their ideas to investors.", new DateTime(2024, 7, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), "Startup Pitch Event", new DateTime(2024, 7, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedOn", "Description", "End", "Name", "OrganiserId", "Start", "TypeId" },
                values: new object[] { 1, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A conference for technology enthusiasts and professionals.", new DateTime(2024, 5, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), "Tech Conference 2024", "30c121b4-41dd-4b57-9448-243bcf213958", new DateTime(2024, 5, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 });
        }
    }
}
