using StudentOrganizer.Core.Common;
using System;

namespace StudentOrganizer.Core.Models
{
	public class Assignment : Entity
	{
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public DateTime? Deadline { get; protected set; }
		public int Semester { get; protected set; }
		public Course Course { get; protected set; }
		public bool HasAssignmentExpired => Deadline != null && Deadline < DateTime.Now;

		protected Assignment()
		{
		}

		public Assignment(string name, string description, int semester, DateTime? deadline, Guid courseId)
		{
			SetValues(name, description, semester, deadline, courseId);
		}

		public void Update(string name, string description, int semester, DateTime? deadline, Guid courseId)
		{
			SetValues(name, description, semester, deadline, courseId);
		}

		private void SetValues(string name, string description, int semester, DateTime? deadline, Guid courseId)
		{
			Course = new Course(courseId);
			SetName(name);
			SetDescription(description);
			SetSemester(semester);
			SetDeadline(deadline);
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new AppException("Name can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			Name = name;
		}

		public void SetDescription(string description)
		{
			if (string.IsNullOrWhiteSpace(description))
			{
				throw new AppException("Description can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			Description = description;
		}

		public void SetSemester(int semester)
		{
			if (semester < 0)
			{
				throw new AppException("Semester can not be lower than zero.", AppErrorCode.VALIDATION_ERROR);
			}
			Semester = semester;
		}

		public void SetDeadline(DateTime? deadline)
		{
			Deadline = deadline;
		}
	}
}