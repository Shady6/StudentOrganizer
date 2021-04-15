using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Team : Entity
	{
		private ISet<User> _students = new HashSet<User>();
		public string Name { get; protected set; }
		public Schedule Schedule { get; protected set; }
		public IEnumerable<User> Students
		{
			get => _students;
			protected set { _students = new HashSet<User>(value); }
		}
		public IList<Assignment> Assignmets { get; protected set; }

		public Team(string name, Schedule schedule)
		{
			SetName(name);
			Schedule = schedule;
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
	}
}