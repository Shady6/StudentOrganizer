using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Group : Entity
	{
		private ISet<User> _administrators = new HashSet<User>();
		private ISet<User> _students = new HashSet<User>();
		public string Name { get; protected set; }

		public IEnumerable<User> Administrators
		{
			get => _administrators;
			protected set { _administrators = new HashSet<User>(value); }
		}

		public IEnumerable<User> Students
		{
			get => _students;
			protected set { _students = new HashSet<User>(value); }
		}
		public List<Schedule> Schedules { get; protected set; }
		public List<Team> Teams { get; protected set; }
		public List<Course> Course { get; protected set; }

		public Group(Guid id, string name)
		{
			SetName(name);
			Id = id;
		}

		public Group(string name, List<Schedule> schedules, List<Team> teams, List<Course> courses)
		{
			SetName(name);
			Schedules = schedules;
			Teams = teams;
			Course = courses;
		}

		public Group()
		{
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new Exception("Name can not be empty.");
			}
			if (name.Length > 200)
			{
				throw new Exception("Name cannot be longer than 200 characters.");
			}
			Name = name;
		}

		public void AddAdministrator(User user)
		{
			_administrators.Add(user);
		}
	}
}