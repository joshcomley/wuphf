using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wuphf.Data.Migrations
{
    public partial class MakeAuditLogByUserNameNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE AuditLogs
SET ByUserName = COALESCE(ToUserName, 'Unknown')
");
            migrationBuilder.AlterColumn<string>(
                name: "ByUserName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ByUserName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
