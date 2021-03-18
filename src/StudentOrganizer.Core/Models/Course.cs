using NodaTime;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Course : Entity
	{
		private ISet<Assignment> _assignments = new HashSet<Assignment>();
		public string Name { get; protected set; }
		public string Lecturer { get; protected set; }
		public Location Location { get; protected set; }
		public IsoDayOfWeek DayOfTheWeek { get; protected set; }
		public LocalTime StartTime { get; protected set; }
		public LocalTime EndTime { get; protected set; }
		public int Semester { get; protected set; }
		public IEnumerable<Assignment> Assignments => _assignments;



	}
}