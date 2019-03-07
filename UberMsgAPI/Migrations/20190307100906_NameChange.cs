using Microsoft.EntityFrameworkCore.Migrations;

namespace UberMsgAPI.Migrations
{
    public partial class NameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty");

            migrationBuilder.RenameTable(
                name: "MyProperty",
                newName: "ActiveUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveUsers",
                table: "ActiveUsers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveUsers",
                table: "ActiveUsers");

            migrationBuilder.RenameTable(
                name: "ActiveUsers",
                newName: "MyProperty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty",
                column: "Id");
        }
    }
}
