using System;
using NodaTime;

namespace StudentOrganizer.Core.Models
{
	public class Course : Entity
	{
		public string Name { get; protected set; }
		public string Lecturer { get; protected set; }
		public Location Location { get; protected set; }
		public IsoDayOfWeek DayOfTheWeek { get; protected set; }
		public LocalTime StartTime { get; protected set; }
		public LocalTime EndTime { get; protected set; }
		public int Semester { get; protected set; }

		public Course(string name, string lecturer, Location location, IsoDayOfWeek day,
			LocalTime startTime, LocalTime endTime, int semester)
		{
			SetName(name);
			SetLecturer(lecturer);
			Location = location;
			DayOfTheWeek = day;
			StartTime = startTime;
			EndTime = endTime;
			SetSemester(semester);
		}

		public Course()
		{
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new Exception("Name can not be empty.");
			}
			Name = name;
		}

		public void SetLecturer(string lecturer)
		{
			if (string.IsNullOrWhiteSpace(lecturer))
			{
				throw new Exception("Lecturer can not be empty.");
			}
			Lecturer = lecturer;
		}

		public void SetSemester(int semester)
		{
			if (semester < 0)
			{
				throw new Exception("Semester can not be lower than zero.");
			}
			Semester = semester;
		}
	}
}