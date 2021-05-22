using StudentOrganizer.Core.Models;
using System;

namespace StudentOrganizer.Core.Behaviors.Assignments
{
	public interface IAssignmentActions
	{
		public void AddAsignment(Assignment assignment);

		public void UpdateAssignment(string name, string description, int semester, DateTime? deadline, Course course, Guid assignmentId);

		public void DeleteAssignment(Guid id);
	}
}