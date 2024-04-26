using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    public partial class fixDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Schedules_ScheduleId",
                table: "Attendances");

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "DayOfWeek", "EndTime", "LessonId", "StartTime", "StudyGroupId", "TeacherId", "TenantId" },
                values: new object[] { new Guid("e463e458-599b-4601-a50e-c52633c44c69"), 1, new TimeSpan(0, 23, 59, 59, 0), new Guid("00000000-0000-0000-0000-000000000001"), new TimeSpan(0, 0, 0, 1, 0), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Password",
                value: "KPixd9BjDhFtv3okqX9D+K5/QJT/3crEe2McTX7lzpR3Lfr4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Password",
                value: "wXDAIB4D9Jvgwui8Zn/kuwdw9KQmDJ6dITsXwi/d3cSEY5xM");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "Password",
                value: "7NxSMg9r5q7AZgLMBEYlt8y1ID1PMbCK9qMVM2t+AsmWpsNy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "Password",
                value: "CEq+9JOJseAQJx466qZuvuPOiMNiqUGktgHeUllhFLAcH0mu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "Password",
                value: "71HQPJ29FFNssBpvu119FzTU9ClJaQ8+XzWKifftTzg9q94o");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "Password",
                value: "35qvlq4VpjXB9WD6aC7AZ3vzJ2fpbiJAvzi6zVj2xlI8PRTS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "Password",
                value: "hCRh8YnsL3QeWNwa6gSrxGYDxaOAMH2pz0yTVTzSkid6sX3S");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Schedules_ScheduleId",
                table: "Attendances",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Schedules_ScheduleId",
                table: "Attendances");

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("e463e458-599b-4601-a50e-c52633c44c69"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Password",
                value: "yBLcueWAWrHTVXfS5mHXmrfqIyDq6VzU+kRxzEcs3gTQgGvn");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Password",
                value: "N14GxEVYEbzE4qzGHEdfoh+YNO+AF94kb4Rh+zZe0/L4e8Kb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "Password",
                value: "ulWkPu/2UMN6wYd2vhqFY5FmzkKtW4i51E5zrbULvtZdHL6I");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "Password",
                value: "rHQGiPlMHBLqR4N+LQlTiszEOCxFzPWSWRBRfdnRI3okFl96");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "Password",
                value: "c6zJgewMydlTk/vCc7ZuVswxWS3BV0O6u887sr440IQajQ1i");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "Password",
                value: "7yX1mkezFXGq3SWUliBe12wc0+oIbpAcNLv6kdbFoCS2j9tp");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "Password",
                value: "mG2TDurtgzAW3VXOh9Z+fPvqa2b4tgazercELudO6h9hfpja");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Schedules_ScheduleId",
                table: "Attendances",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
