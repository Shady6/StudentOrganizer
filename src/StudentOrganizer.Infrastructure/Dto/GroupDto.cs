using System.Collections.Generic;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class GroupDto
	{
		public List<DisplayUserDto> Administrators { get; set; }
		public List<DisplayUserDto> Students { get; set; }
		public string Name { get; set; }
		public List<ScheduleDto> Schedules { get; set; }
		public List<Team> Teams { get; set; }
		public List<CourseDto> Course { get; set; }
	}
}