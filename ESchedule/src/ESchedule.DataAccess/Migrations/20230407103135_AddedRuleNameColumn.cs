using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    public partial class AddedRuleNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RuleName",
                table: "ScheduleRules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuleName",
                table: "ScheduleRules");
        }
    }
}
