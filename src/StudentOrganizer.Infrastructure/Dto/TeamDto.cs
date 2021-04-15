using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class TeamDto
	{
		public string Name { get; set; }
		public ScheduleDto Schedule { get; set; }
		public List<AssignmentDto> Assignments { get; set; }
	}
}