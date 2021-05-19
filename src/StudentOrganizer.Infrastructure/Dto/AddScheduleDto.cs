using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class AddScheduleDto
	{
		public List<AddScheduledCourseDto> ScheduledCourses { get; set; }
		public int Semester { get; set; }
	}
}