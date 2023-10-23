using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalProject.Migrations
{
    public partial class adminStuff5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4280b6a-0613-4cbd-a9e6-f1701e926e73",
                column: "ConcurrencyStamp",
                value: "6d4c5cbe-52b5-4f91-a88a-25c7a09656c4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4280b6a-0613-4cbd-a9e6-f1701e926e73",
                column: "ConcurrencyStamp",
                value: "8c9fabd3-32a4-4f02-90f7-15a3aaa59652");
        }
    }
}
