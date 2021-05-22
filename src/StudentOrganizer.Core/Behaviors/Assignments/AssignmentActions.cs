using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentOrganizer.Core.Behaviors.Assignments
{
	public class AssignmentActions : IAssignmentActions
	{
		public IList<Assignment> Assignmets { get; set; } = new List<Assignment>();

		public void AddAsignment(Assignment assignment)
		{
			if (Assignmets.Any(a => a.Name == assignment.Name && a.Semester == assignment.Semester))
				throw new AppException($"Assignment with name {assignment.Name} in semester {assignment.Semester} already exists", AppErrorCode.ALREADY_EXISTS);
			Assignmets.Add(assignment);
		}

		public void DeleteAssignment(Guid id)
		{
			var assignmentToDelete = Assignmets.FirstOrDefault(a => a.Id == id);
			if (assignmentToDelete == null)
				throw new AppException("Assignment you're trying to delete doesn't exist", AppErrorCode.DOESNT_EXIST);
			Assignmets.Remove(assignmentToDelete);
		}

		public void UpdateAssignment(string name, string description, int semester, DateTime? deadline, Course course, Guid assignmentId)
		{
			var assignmentToUpdate = Assignmets.FirstOrDefault(a => a.Id == assignmentId);
			if (assignmentToUpdate == null)
				throw new AppException("Assignment you're trying to update doesn't exist", AppErrorCode.DOESNT_EXIST);
			assignmentToUpdate.Update(name, description, semester, deadline, course);
		}
	}
}