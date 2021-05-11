using NodaTime;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class ScheduledCourseDto
	{
		public IsoDayOfWeek DayOfTheWeek { get; set; }
		public LocalTime StartTime { get; set; }
		public LocalTime EndTime { get; set; }
		public Identifier Course { get; set; }
	}
	
}