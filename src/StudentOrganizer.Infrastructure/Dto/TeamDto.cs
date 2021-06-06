using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class TeamDto
	{
		public string Name { get; set; }
		public List<ScheduleDto> Schedules { get; set; }
		public List<AssignmentDto> Assignments { get; set; }
		public List<DisplayUserDto> Students { get; set; }
	}
}