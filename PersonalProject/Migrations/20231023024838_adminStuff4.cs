using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalProject.Migrations
{
    public partial class adminStuff4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4280b6a-0613-4cbd-a9e6-f1701e926e73",
                column: "ConcurrencyStamp",
                value: "8c9fabd3-32a4-4f02-90f7-15a3aaa59652");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4280b6a-0613-4cbd-a9e6-f1701e926e73",
                column: "ConcurrencyStamp",
                value: "f80ad1fc-4f8b-4a3f-be4f-a59ef6398129");
        }
    }
}
