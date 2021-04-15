using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class ScheduleDto
	{
		public List<CourseDto> Courses { get; set; }
		public int Semester { get; set; }
	}
}