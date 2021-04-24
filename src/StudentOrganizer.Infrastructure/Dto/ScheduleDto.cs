using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class ScheduleDto
	{
		public List<ScheduledCourseDto> Courses { get; set; }
		public int Semester { get; set; }
	}
}