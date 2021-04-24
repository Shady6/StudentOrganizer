using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentOrganizer.Infrastructure.Migrations
{
    public partial class AddScheduledCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Schedule_ScheduleId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_ScheduleId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "DayOfTheWeek",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "ScheduledCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfTheWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledCourse_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledCourse_CourseId",
                table: "ScheduledCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledCourse_ScheduleId",
                table: "ScheduledCourse",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledCourse");

            migrationBuilder.AddColumn<int>(
                name: "DayOfTheWeek",
                table: "Course",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Course",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Course",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Course",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Course_ScheduleId",
                table: "Course",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Schedule_ScheduleId",
                table: "Course",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
