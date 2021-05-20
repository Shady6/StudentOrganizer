using StudentOrganizer.Core.Behaviors.Assignments;
using StudentOrganizer.Core.Behaviors.RemoveBehaviors;
using StudentOrganizer.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentOrganizer.Core.Models
{
	public class Team : Entity, IAssignmentActions
	{
		private ISet<User> _students = new HashSet<User>();
		public string Name { get; protected set; }
		public IList<Schedule> Schedules { get; protected set; } = new List<Schedule>();

		public IEnumerable<User> Students
		{
			get => _students;
			protected set { _students = new HashSet<User>(value); }
		}

		private readonly AssignmentActions AssignmentActions = new();

		public IList<Assignment> Assignmets
		{
			get => AssignmentActions.Assignmets;

			protected set => AssignmentActions.Assignmets = value;
		}

		public Team(string name)
		{
			SetName(name);
		}

		public Team()
		{
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new AppException("Name can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			Name = name;
		}

		public void AddSchedule(Schedule schedule)
		{
			if (Schedules.Any(s => s.Semester == schedule.Semester))
				throw new AppException($"Schedule for semester {schedule.Semester} already exists, update the existing one or delete and then add.", AppErrorCode.ALREADY_EXISTS);

			Schedules.Add(schedule);
		}

		public void DeleteSchedule(int semester)
		{
			var foundSchedule = Schedules.FirstOrDefault(s => s.Semester == semester);
			if (foundSchedule == null)
				throw new AppException($"Schedule for semester {semester} doesn't exist.", AppErrorCode.DOESNT_EXIST);
			Schedules.Remove(foundSchedule);
		}

		public void AddStudents(List<User> groupStudents)
		{
			var usersToAdd = groupStudents.Where(u => !_students.Select(s => s.Id).Contains(u.Id));
			_students.UnionWith(groupStudents);
		}

		public void RemoveStudents(List<string> emails, Guid userId)
		{
			IRemoveUsersBehavior removeStudentsBehavior =
				new RemoveBehavior(_students, "Team", "Students");
			removeStudentsBehavior.Remove(emails, userId);
		}

		public void AddAsignment(Assignment assignment)
			=> AssignmentActions.AddAsignment(assignment);

		public void UpdateAssignment(string name, string description, int semester, DateTime? deadline, Guid courseId, Guid assignmentId)
			=> AssignmentActions.UpdateAssignment(name, description, semester, deadline, courseId, assignmentId);

		public void DeleteAssignment(Guid id)
			=> AssignmentActions.DeleteAssignment(id);
	}
}