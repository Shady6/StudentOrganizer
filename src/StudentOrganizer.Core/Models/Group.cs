using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Group : Entity
	{
		private ISet<User> _administrators = new HashSet<User>();
		private ISet<User> _students = new HashSet<User>();
		public IEnumerable<User> Administrators => _administrators;
		public IEnumerable<User> Students => _students;
		public List<Schedule> Schedules { get; set; }
		public List<Team> Teams { get; set; }
		public List<Course> Course { get; set; }		
	}
}