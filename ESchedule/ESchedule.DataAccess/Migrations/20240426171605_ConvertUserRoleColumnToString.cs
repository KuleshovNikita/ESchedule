using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    public partial class ConvertUserRoleColumnToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "h8TRaqXaxa7ST8aFyCQL3lbR09uexQ5ECgx6wWgJPHtlBp6W", "Pupil" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "h8TRaqXaxa7ST8aFyCQL3lbR09uexQ5ECgx6wWgJPHtlBp6W", "Pupil" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "h8TRaqXaxa7ST8aFyCQL3lbR09uexQ5ECgx6wWgJPHtlBp6W", "Pupil" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "h8TRaqXaxa7ST8aFyCQL3lbR09uexQ5ECgx6wWgJPHtlBp6W", "Pupil" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "h8TRaqXaxa7ST8aFyCQL3lbR09uexQ5ECgx6wWgJPHtlBp6W", "Teacher" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "h8TRaqXaxa7ST8aFyCQL3lbR09uexQ5ECgx6wWgJPHtlBp6W", "Teacher" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "h8TRaqXaxa7ST8aFyCQL3lbR09uexQ5ECgx6wWgJPHtlBp6W", "Teacher" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "KPixd9BjDhFtv3okqX9D+K5/QJT/3crEe2McTX7lzpR3Lfr4", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "wXDAIB4D9Jvgwui8Zn/kuwdw9KQmDJ6dITsXwi/d3cSEY5xM", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "7NxSMg9r5q7AZgLMBEYlt8y1ID1PMbCK9qMVM2t+AsmWpsNy", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "CEq+9JOJseAQJx466qZuvuPOiMNiqUGktgHeUllhFLAcH0mu", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "71HQPJ29FFNssBpvu119FzTU9ClJaQ8+XzWKifftTzg9q94o", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "35qvlq4VpjXB9WD6aC7AZ3vzJ2fpbiJAvzi6zVj2xlI8PRTS", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "Password", "Role" },
                values: new object[] { "hCRh8YnsL3QeWNwa6gSrxGYDxaOAMH2pz0yTVTzSkid6sX3S", 1 });
        }
    }
}
