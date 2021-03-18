using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Schedule
	{
		public ISet<Course> _courses = new HashSet<Course>();
		public IEnumerable<Course> Courses => _courses;
		public int Semester { get; protected set; }
	}
}