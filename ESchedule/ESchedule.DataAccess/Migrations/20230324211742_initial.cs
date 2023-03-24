using System;
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
                name: "LessonModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentialsModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentialsModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MaxLessonsCountPerDay = table.Column<int>(type: "int", nullable: false),
                    MasterTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupModel_TeacherModel_MasterTeacherId",
                        column: x => x.MasterTeacherId,
                        principalTable: "TeacherModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettingsModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyDayStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LessonDurationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreaksDurationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingsModel_TeacherModel_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "TeacherModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_TeachersLessonsModel_TeacherModel_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherModel",
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
                name: "PupilModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PupilModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PupilModel_GroupModel_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_ScheduleModel_TeacherModel_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherModel",
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
                        name: "FK_TeachersGroupsModel_TeacherModel_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupModel_MasterTeacherId",
                table: "GroupModel",
                column: "MasterTeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupModel_Title",
                table: "GroupModel",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupsLessonsModel_LessonId",
                table: "GroupsLessonsModel",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsLessonsModel_StudyGroupId",
                table: "GroupsLessonsModel",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonModel_Title",
                table: "LessonModel",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PupilModel_GroupId",
                table: "PupilModel",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PupilModel_Login",
                table: "PupilModel",
                column: "Login",
                unique: true);

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
                name: "IX_SettingsModel_CreatorId",
                table: "SettingsModel",
                column: "CreatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherModel_Login",
                table: "TeacherModel",
                column: "Login",
                unique: true);

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
                name: "IX_UserCredentialsModel_Login",
                table: "UserCredentialsModel",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsLessonsModel");

            migrationBuilder.DropTable(
                name: "PupilModel");

            migrationBuilder.DropTable(
                name: "ScheduleModel");

            migrationBuilder.DropTable(
                name: "SettingsModel");

            migrationBuilder.DropTable(
                name: "TeachersGroupsModel");

            migrationBuilder.DropTable(
                name: "TeachersLessonsModel");

            migrationBuilder.DropTable(
                name: "UserCredentialsModel");

            migrationBuilder.DropTable(
                name: "GroupModel");

            migrationBuilder.DropTable(
                name: "LessonModel");

            migrationBuilder.DropTable(
                name: "TeacherModel");
        }
    }
}
