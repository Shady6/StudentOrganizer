using NodaTime;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Course : Entity
	{
		private ISet<Assignment> _assignments = new HashSet<Assignment>();
		public string Name { get; set; }
		public string Lecturer { get; set; }
		public Location Location { get; set; }
		public IsoDayOfWeek DayOfTheWeek { get; set; }
		public LocalTime StartTime { get; set; }
		public LocalTime EndTime { get; set; }
		public IEnumerable<Assignment> Assignments => _assignments;
		public int Semester { get; protected set; }
	}
}