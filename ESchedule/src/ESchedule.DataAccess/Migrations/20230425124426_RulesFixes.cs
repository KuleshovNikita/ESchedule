using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    public partial class RulesFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleRules_Tenant_TenantId",
                table: "ScheduleRules");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Password",
                value: "2vn/dQqVmtXQ/j19M/qkIJp0mmF0qKe3d5PadfLSL2FXe8/X");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Password",
                value: "d5+RxcOM3fxNo5mIKUdU8B4Kvp9hZhW3hcgR8My1pqg8aSDp");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "Password",
                value: "7eIn7uY0q1dFMjvZ47883mCtAHo1DX/3gc5mCDLBF9hIhyxT");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "Password",
                value: "HiyxuaPxtoulxaMZUnlND1DCkw22XQoAt0WhPV1IKrk1/zhF");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "Password",
                value: "87GBOqsiuzIC4prkUQEdPnmNQioNeZLhmAxkJUq/01fbX2/L");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "Password",
                value: "byV6D9LM+/zLGJ0vAvO1LxZot9nrYEe6JqlDw3C6Jw3V8pwy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "Password",
                value: "h9p46uHoopPYE7rKQlL/uT8MUnchw03cYI+LANnk/+PIO6Iq");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRules_Tenant_TenantId",
                table: "ScheduleRules",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleRules_Tenant_TenantId",
                table: "ScheduleRules");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Password",
                value: "y8VZmuHG1n8AtQjmkTIfo9XqzKF8lGxIMyD3yo8+fCSD7wJG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Password",
                value: "nAAhHoGAVLcBd3TS9wtMR25qYeuHXK5ZFqxysDjUKgZai3Sf");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "Password",
                value: "LDV4sVHqm0cW9DOyB4s2bid2hBUGZ7YIXpG8zr+AUADPfxQX");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "Password",
                value: "shUespHSIRERoj2kQUX5fcVnmqf5NCdxTZgSEdUanfDBl7YB");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "Password",
                value: "32B+k2TO8V0mRFYGCZUPK6usIYFVYZzIO9zHIOnpkjyIeQg+");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "Password",
                value: "vlCnIhDKDKA5CXiuUt4amYZE30o6CxUH46UZQ/mG5U74usw4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "Password",
                value: "U0HASmOMw5nc0QegRgJzk+WDM6djk8swjA/ze2viKGVy3dX9");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRules_Tenant_TenantId",
                table: "ScheduleRules",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");
        }
    }
}
