using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    public partial class CascadeLessonRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Tenant_TenantId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsLessons_Groups_StudyGroupId",
                table: "GroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsLessons_Lessons_LessonId",
                table: "GroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Groups_StudyGroupId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Lessons_LessonId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_TeacherId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersGroupsLessons_Groups_StudyGroupId",
                table: "TeachersGroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersGroupsLessons_Lessons_LessonId",
                table: "TeachersGroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersGroupsLessons_Users_TeacherId",
                table: "TeachersGroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_Users_TeacherId",
                table: "TeachersLessons");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Tenant_TenantId",
                table: "Groups",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsLessons_Groups_StudyGroupId",
                table: "GroupsLessons",
                column: "StudyGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsLessons_Lessons_LessonId",
                table: "GroupsLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Groups_StudyGroupId",
                table: "Schedules",
                column: "StudyGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Lessons_LessonId",
                table: "Schedules",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_TeacherId",
                table: "Schedules",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersGroupsLessons_Groups_StudyGroupId",
                table: "TeachersGroupsLessons",
                column: "StudyGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersGroupsLessons_Lessons_LessonId",
                table: "TeachersGroupsLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersGroupsLessons_Users_TeacherId",
                table: "TeachersGroupsLessons",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_Users_TeacherId",
                table: "TeachersLessons",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Tenant_TenantId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsLessons_Groups_StudyGroupId",
                table: "GroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsLessons_Lessons_LessonId",
                table: "GroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Groups_StudyGroupId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Lessons_LessonId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_TeacherId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersGroupsLessons_Groups_StudyGroupId",
                table: "TeachersGroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersGroupsLessons_Lessons_LessonId",
                table: "TeachersGroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersGroupsLessons_Users_TeacherId",
                table: "TeachersGroupsLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_Users_TeacherId",
                table: "TeachersLessons");

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
                name: "FK_Groups_Tenant_TenantId",
                table: "Groups",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsLessons_Groups_StudyGroupId",
                table: "GroupsLessons",
                column: "StudyGroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsLessons_Lessons_LessonId",
                table: "GroupsLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Groups_StudyGroupId",
                table: "Schedules",
                column: "StudyGroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Lessons_LessonId",
                table: "Schedules",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_TeacherId",
                table: "Schedules",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersGroupsLessons_Groups_StudyGroupId",
                table: "TeachersGroupsLessons",
                column: "StudyGroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersGroupsLessons_Lessons_LessonId",
                table: "TeachersGroupsLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersGroupsLessons_Users_TeacherId",
                table: "TeachersGroupsLessons",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_Users_TeacherId",
                table: "TeachersLessons",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
