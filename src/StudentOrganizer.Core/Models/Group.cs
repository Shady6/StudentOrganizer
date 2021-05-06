using System;
using System.Collections.Generic;
using System.Linq;
using StudentOrganizer.Core.Common;

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
				throw new AppException($"These courses weren't added because they already exist:\n" +
					$"{string.Join('\n', coursesAlreadyExisting)}", AppErrorCode.ALREADY_EXISTS);
		}

		public void DeleteCourse(Guid courseId)
		{
			var course = Courses.FirstOrDefault(c => c.Id == courseId);
			if (course == null)
				throw new AppException("The course you're trying to delete doesn't exist.", AppErrorCode.DOESNT_EXIST);
			Courses.Remove(course);
		}

		public void UpdateCourse(Course course)
		{
			var foundCourse = Courses.FirstOrDefault(c => c.Id == course.Id);
			if (foundCourse == null)
				throw new AppException("The course you're trying to update doesn't exist.", AppErrorCode.DOESNT_EXIST);
			else if (Courses.Any(c => c.Equals(course)))
				throw new AppException("The course with provided values already exists.", AppErrorCode.ALREADY_EXISTS);
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
				throw new AppException($"These teams weren't added because they already exist:\n" +
					$"{string.Join('\n', teamsAreadyExisting)}", AppErrorCode.ALREADY_EXISTS);
		}

		public void DeleteTeam(string teamName)
		{
			var team = Teams.FirstOrDefault(t => t.Name == teamName);
			if (team == null)
				throw new AppException("The team you're trying to delete doesn't exist.", AppErrorCode.DOESNT_EXIST);
			Teams.Remove(team);
		}

		public void UpdateTeamName(string teamName, string newTeamName)
		{
			var team = Teams.FirstOrDefault(t => t.Name == teamName);
			if (team == null)
				throw new AppException("The team you're trying to update doesn't exist.", AppErrorCode.DOESNT_EXIST);
			team.SetName(newTeamName);
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new AppException("Name can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			if (name.Length > 200)
			{
				throw new AppException("Name cannot be longer than 200 characters.", AppErrorCode.VALIDATION_ERROR);
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
	}
}