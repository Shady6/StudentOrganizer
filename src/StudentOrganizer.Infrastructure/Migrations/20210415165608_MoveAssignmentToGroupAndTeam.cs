using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentOrganizer.Infrastructure.Migrations
{
    public partial class MoveAssignmentToGroupAndTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Course_CourseId",
                table: "Assignment");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Assignment",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_CourseId",
                table: "Assignment",
                newName: "IX_Assignment_TeamId");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Assignment",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_GroupId",
                table: "Assignment",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Group_GroupId",
                table: "Assignment",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Team_TeamId",
                table: "Assignment",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Group_GroupId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Team_TeamId",
                table: "Assignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_GroupId",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Assignment");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Assignment",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_TeamId",
                table: "Assignment",
                newName: "IX_Assignment_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Course_CourseId",
                table: "Assignment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
