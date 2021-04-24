using NodaTime;

namespace StudentOrganizer.Core.Models
{
	public class ScheduledCourse : Entity
	{
		public IsoDayOfWeek DayOfTheWeek { get; protected set; }
		public LocalTime StartTime { get; protected set; }
		public LocalTime EndTime { get; protected set; }
		public Course Course { get; set; }

		public ScheduledCourse()
		{

		}
	}
}