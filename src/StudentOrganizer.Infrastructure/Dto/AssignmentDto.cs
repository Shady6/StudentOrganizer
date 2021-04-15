using System;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class AssignmentDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? Deadline { get; set; }
		public int Semester { get; set; }
	}
}