using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalProject.Migrations
{
    public partial class notSureWhat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4280b6a-0613-4cbd-a9e6-f1701e926e73",
                column: "ConcurrencyStamp",
                value: "52d702fc-497d-4b96-9782-015943f5a46d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4280b6a-0613-4cbd-a9e6-f1701e926e73",
                column: "ConcurrencyStamp",
                value: "0fb277c4-120a-49b8-80f3-a0543c86830d");
        }
    }
}
