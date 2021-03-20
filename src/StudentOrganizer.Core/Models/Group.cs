using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Group : Entity
	{
		private ISet<User> _administrators = new HashSet<User>();
		private ISet<User> _students = new HashSet<User>();
		public string Name { get; protected set; }
		public IEnumerable<User> Administrators => _administrators;
		public IEnumerable<User> Students => _students;
		public List<Schedule> Schedules { get; protected set; }
		public List<Team> Teams { get; protected set; }
		public List<Course> Course { get; protected set; }

        public Group(string name, List<Schedule> schedules, List<Team> teams, List<Course> courses)
        {
			SetName(name);
			Schedules = schedules;
			Teams = teams;
			Course = courses;
        }
		public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
				throw new Exception("Name can not be empty.");
            }
			Name = name;
        }
	}
}