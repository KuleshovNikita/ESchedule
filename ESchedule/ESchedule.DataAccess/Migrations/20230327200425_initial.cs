﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MaxLessonsCountPerDay = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupModel_TenantModel_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonModel_TenantModel_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantSettingsModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyDayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LessonDurationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreaksDurationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSettingsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSettingsModel_TenantModel_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModel_GroupModel_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserModel_TenantModel_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupsLessonsModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsLessonsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsLessonsModel_GroupModel_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "GroupModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupsLessonsModel_LessonModel_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StudyGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleModel_GroupModel_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "GroupModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduleModel_LessonModel_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduleModel_TenantModel_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduleModel_UserModel_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeachersGroupsModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersGroupsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachersGroupsModel_GroupModel_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "GroupModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeachersGroupsModel_UserModel_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeachersLessonsModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersLessonsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachersLessonsModel_LessonModel_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeachersLessonsModel_UserModel_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "UserModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupModel_TenantId",
                table: "GroupModel",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsLessonsModel_LessonId",
                table: "GroupsLessonsModel",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsLessonsModel_StudyGroupId",
                table: "GroupsLessonsModel",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonModel_TenantId",
                table: "LessonModel",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_LessonId",
                table: "ScheduleModel",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_StudyGroupId",
                table: "ScheduleModel",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_TeacherId",
                table: "ScheduleModel",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_TenantId",
                table: "ScheduleModel",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersGroupsModel_StudyGroupId",
                table: "TeachersGroupsModel",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersGroupsModel_TeacherId",
                table: "TeachersGroupsModel",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersLessonsModel_LessonId",
                table: "TeachersLessonsModel",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersLessonsModel_TeacherId",
                table: "TeachersLessonsModel",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantModel_TenantName",
                table: "TenantModel",
                column: "TenantName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettingsModel_TenantId",
                table: "TenantSettingsModel",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_GroupId",
                table: "UserModel",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_Login",
                table: "UserModel",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_TenantId",
                table: "UserModel",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsLessonsModel");

            migrationBuilder.DropTable(
                name: "ScheduleModel");

            migrationBuilder.DropTable(
                name: "TeachersGroupsModel");

            migrationBuilder.DropTable(
                name: "TeachersLessonsModel");

            migrationBuilder.DropTable(
                name: "TenantSettingsModel");

            migrationBuilder.DropTable(
                name: "LessonModel");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.DropTable(
                name: "GroupModel");

            migrationBuilder.DropTable(
                name: "TenantModel");
        }
    }
}