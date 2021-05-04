using System;
using NodaTime;

namespace StudentOrganizer.Core.Models
{
	public class ScheduledCourse : Entity
	{
		public IsoDayOfWeek DayOfTheWeek { get; protected set; }
		public LocalTime StartTime { get; protected set; }
		public LocalTime EndTime { get; protected set; }
		public Course Course { get; protected set; }

		public ScheduledCourse(IsoDayOfWeek dayOfTheWeek, LocalTime startTime, LocalTime endTime, Course course)
		{
			if (startTime > endTime)
				throw new Exception("Start time of the course cannot be after the end time.");

			DayOfTheWeek = dayOfTheWeek;
			StartTime = startTime;
			EndTime = endTime;
			Course = course;
		}

		public ScheduledCourse()
		{

		}
	}
}