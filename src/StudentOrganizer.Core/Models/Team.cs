using System;
using System.Collections.Generic;
using System.Linq;
using StudentOrganizer.Core.Common;

namespace StudentOrganizer.Core.Models
{
	public class Team : Entity
	{
		private ISet<User> _students = new HashSet<User>();
		public string Name { get; protected set; }
		public IList<Schedule> Schedules { get; protected set; } = new List<Schedule>();
		public IEnumerable<User> Students
		{
			get => _students;
			protected set { _students = new HashSet<User>(value); }
		}
		public IList<Assignment> Assignmets { get; protected set; } = new List<Assignment>();

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

        public void AddAssignment(Assignment assignment)
        {
            if (Assignmets.Any(s => s.Id.Equals(assignment.Id)))
                throw new AppException($"Assignment {assignment.Id} already exists, update the existing one or delete and then add.", AppErrorCode.ALREADY_EXISTS);

            Assignmets.Add(assignment);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            var foundAssignment = Assignmets.FirstOrDefault(s => s.Id.Equals(assignment.Id));
            if (foundAssignment == null)
                throw new AppException($"Assignment {assignment.Id} doesn't exist.", AppErrorCode.DOESNT_EXIST);

            Assignmets.Remove(foundAssignment);
        }

        public void AddStudents(List<User> groupStudents)
        {
			var usersToAdd = groupStudents.Where(u => !_students.Select(s => s.Id).Contains(u.Id));
			_students.UnionWith(groupStudents);
		}
	}
}
