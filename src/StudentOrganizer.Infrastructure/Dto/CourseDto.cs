using NodaTime;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class CourseDto
	{
		public string Name { get; set; }
		public string Lecturer { get; set; }
		public LocationDto Location { get; set; }
		public IsoDayOfWeek DayOfTheWeek { get; set; }
		public LocalTime StartTime { get; set; }
		public LocalTime EndTime { get; set; }
		public int Semester { get; set; }
	}
}