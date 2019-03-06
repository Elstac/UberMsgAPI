using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UberMsgAPI.Migrations
{
    public partial class AddSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
                System.Diagnostics.Debugger.Launch();

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Passwords",
                nullable: true);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Passwords");
        }
    }
}
