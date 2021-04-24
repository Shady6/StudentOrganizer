using System;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class SmallGroupDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int StudentsCount { get; set; }
		public int AssignmentsInProgress { get; set; }
	}
}