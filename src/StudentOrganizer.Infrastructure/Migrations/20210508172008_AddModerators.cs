using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentOrganizer.Infrastructure.Migrations
{
    public partial class AddModerators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser1_Group_GroupsId",
                table: "GroupUser1");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser1_Users_StudentsId",
                table: "GroupUser1");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "GroupUser1",
                newName: "ModeratorsId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupUser1",
                newName: "ModeratedGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser1_StudentsId",
                table: "GroupUser1",
                newName: "IX_GroupUser1_ModeratorsId");

            migrationBuilder.CreateTable(
                name: "GroupUser2",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser2", x => new { x.GroupsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_GroupUser2_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser2_Users_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser2_StudentsId",
                table: "GroupUser2",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser1_Group_ModeratedGroupsId",
                table: "GroupUser1",
                column: "ModeratedGroupsId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser1_Users_ModeratorsId",
                table: "GroupUser1",
                column: "ModeratorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser1_Group_ModeratedGroupsId",
                table: "GroupUser1");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser1_Users_ModeratorsId",
                table: "GroupUser1");

            migrationBuilder.DropTable(
                name: "GroupUser2");

            migrationBuilder.RenameColumn(
                name: "ModeratorsId",
                table: "GroupUser1",
                newName: "StudentsId");

            migrationBuilder.RenameColumn(
                name: "ModeratedGroupsId",
                table: "GroupUser1",
                newName: "GroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser1_ModeratorsId",
                table: "GroupUser1",
                newName: "IX_GroupUser1_StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser1_Group_GroupsId",
                table: "GroupUser1",
                column: "GroupsId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser1_Users_StudentsId",
                table: "GroupUser1",
                column: "StudentsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
