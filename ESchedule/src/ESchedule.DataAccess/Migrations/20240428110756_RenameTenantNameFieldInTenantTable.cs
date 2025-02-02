using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    public partial class RenameTenantNameFieldInTenantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantName",
                table: "Tenant",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Tenant_TenantName",
                table: "Tenant",
                newName: "IX_Tenant_Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tenant",
                newName: "TenantName");

            migrationBuilder.RenameIndex(
                name: "IX_Tenant_Name",
                table: "Tenant",
                newName: "IX_Tenant_TenantName");
        }
    }
}
