using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wuphf.Data.Migrations
{
    public partial class AddAuditLogByUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ByUserName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByUserName",
                table: "AuditLogs");
        }
    }
}
