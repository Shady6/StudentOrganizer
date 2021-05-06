using System;
using System.Collections.Generic;
using System.Linq;

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

		public IList<Schedule> Schedules { get; protected set; }
		public IList<Team> Teams { get; protected set; }
		public IList<Course> Courses { get; protected set; }
		public IList<Assignment> Assignmets { get; protected set; }

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
			Courses = courses;
		}

		public Group()
		{
		}

		public void AddCourses(IEnumerable<Course> courses)
		{
			List<string> coursesAlreadyExisting = new();
			foreach (var course in courses)
			{
				if (Courses.Any(c => c.Equals(course)))
					coursesAlreadyExisting.Add(course.Name);					
				else
					Courses.Add(course);
			}
			if (coursesAlreadyExisting.Count != 0)
			{
				var message = "These courses weren't added because they already exist:\n";
				foreach (var existingCourse in coursesAlreadyExisting)
				{
					message += existingCourse + "\n";
				}
				throw new Exception(message);
			}				
		}

		public void DeleteCourse(Guid courseId)
		{
			var course = Courses.FirstOrDefault(c => c.Id == courseId);
			if (course == null)
				throw new Exception("The course you're trying to delete doesn't exist.");
			Courses.Remove(course);
		}

		public void UpdateCourse(Course course)
		{
			var foundCourse = Courses.FirstOrDefault(c => c.Id == course.Id);
			if (foundCourse == null)
				throw new Exception("The course you're trying to update doesn't exist.");
			else if (Courses.Any(c => c.Equals(course)))
				throw new Exception("The course with provided values already exists.");
			foundCourse.Update(course);
		}

		public void AddTeams(IEnumerable<Team> teams)
		{
			List<string> teamsAreadyExisting = new();
			foreach (var team in teams)
			{
				if (Teams.Any(t => t.Name == team.Name))
					teamsAreadyExisting.Add(team.Name);
				else
					Teams.Add(team);
			}
			if (teamsAreadyExisting.Count != 0)
			{
				var message = "These teams weren't added because they already exist:\n";
				foreach (var existingTeam in teamsAreadyExisting)
				{
					message += existingTeam + "\n";
				}
				throw new Exception(message);
			}
		}

		public void DeleteTeam(string teamName)
		{
			var team = Teams.FirstOrDefault(t => t.Name == teamName);
			if (team == null)
				throw new Exception("The team you're trying to delete doesn't exist.");
			Teams.Remove(team);
		}

		public void UpdateTeamName(string teamName, string newTeamName)
		{
			var team = Teams.FirstOrDefault(t => t.Name == teamName);
			if (team == null)
				throw new Exception("The team you're trying to update doesn't exist.");
			team.SetName(newTeamName);
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

		public void AddStudent(User user)
		{
			_students.Add(user);
		}

		public void AddStudents(List<User> users)
        {
            var usersToAdd = users.Where(u => !_students.Select(s => s.Id).Contains(u.Id));
			_students.UnionWith(usersToAdd);
        }
	}
}