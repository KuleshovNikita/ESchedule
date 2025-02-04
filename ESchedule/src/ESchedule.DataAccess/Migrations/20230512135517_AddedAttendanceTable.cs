using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    public partial class AddedAttendanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PupilId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attendances_Users_PupilId",
                        column: x => x.PupilId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_PupilId",
                table: "Attendances",
                column: "PupilId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ScheduleId",
                table: "Attendances",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_TenantId",
                table: "Attendances",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Password",
                value: "mpbgEWFnRU/LZTkVuBpFa/erUQnN6hZ0q4LfDrmzLl0Kwpzi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Password",
                value: "NO1pdnwBZJZgSwcrQxp3oAbP/6J1dCe4apLsePyc4QmBdu0D");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "Password",
                value: "updXx21ZOCy74EyzyGir9B8jq294kIXdlaaT61Rl41NYDz2n");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "Password",
                value: "gGUAJiTK20FiIqoL+qXsiYnzw4iXJvpmTyBWc3KVSQBnfhH7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "Password",
                value: "DxTm0Lp3fW4Vnhor55ml2chYXnO2eCPoDfIpkIxYncdSoBiK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "Password",
                value: "JDPNsKgOUQRuZtDkM1kSBGjYlFvJZDgFa3EeVHvuHzqSf6bf");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "Password",
                value: "RHNKXZ5iTWEGo0RNR1LQkG5z7Eaw2zoWL5SI2h5be5Ri501s");
        }
    }
}
