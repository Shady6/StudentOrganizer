using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Group : Entity
	{
		private ISet<Team> _administrators = new HashSet<Team>();
		private ISet<Team> _students = new HashSet<Team>();
		public string Name { get; protected set; }
		public IEnumerable<Team> Administrators => _administrators;
		public IEnumerable<Team> Students => _students;
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