using System;

namespace StudentOrganizer.Core.Models
{
	public class Assignment : Entity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? Deadline { get; set; }
		public int Semester { get; protected set; }
	}
}