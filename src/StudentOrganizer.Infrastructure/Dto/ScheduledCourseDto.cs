using NodaTime;
using System;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class ScheduledCourseDto
	{
		public Guid Id { get; set; }
		public IsoDayOfWeek DayOfTheWeek { get; set; }
		public LocalTime StartTime { get; set; }
		public LocalTime EndTime { get; set; }
		public CourseDto Course { get; set; }
	}
}