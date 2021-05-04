using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentOrganizer.Core.Models
{
	public class Team : Entity
	{
		private ISet<User> _students = new HashSet<User>();
		public string Name { get; protected set; }
		public IList<Schedule> Schedules { get; protected set; } = new List<Schedule>();
		public IEnumerable<User> Students
		{
			get => _students;
			protected set { _students = new HashSet<User>(value); }
		}
		public IList<Assignment> Assignmets { get; protected set; } = new List<Assignment>();

		public Team(string name)
		{
			SetName(name);			
		}

		public Team()
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

		public void AddSchedule(Schedule schedule)
		{
			if (Schedules.Any(s => s.Semester == schedule.Semester))
				throw new Exception($"Schedule for semester {schedule.Semester} already exists, update the existing one or delete and then add.");

			Schedules.Add(schedule);
		}

		public void DeleteSchedule(int semester)
		{
			var foundSchedule = Schedules.FirstOrDefault(s => s.Semester == semester);
			if (foundSchedule == null)
				throw new Exception($"Schedule for semester {semester} doesn't exist.");
			Schedules.Remove(foundSchedule);
		}
	}
}