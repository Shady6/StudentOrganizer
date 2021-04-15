using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class GroupDto
	{
		public List<DisplayUserDto> Administrators { get; set; }
		public List<StudentDto> Students { get; set; }
		public string Name { get; set; }
		public List<ScheduleDto> Schedules { get; set; }
		public List<TeamDto> Teams { get; set; }
		public List<CourseDto> Course { get; set; }
		public List<AssignmentDto> Assignments { get; set; }
	}
}