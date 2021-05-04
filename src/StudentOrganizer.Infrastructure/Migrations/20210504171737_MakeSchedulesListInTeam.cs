using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentOrganizer.Infrastructure.Migrations
{
    public partial class MakeSchedulesListInTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Schedule_ScheduleId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_ScheduleId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Team");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Schedule",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TeamId",
                table: "Schedule",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Team_TeamId",
                table: "Schedule",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Team_TeamId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_TeamId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Schedule");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Team",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_ScheduleId",
                table: "Team",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Schedule_ScheduleId",
                table: "Team",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
