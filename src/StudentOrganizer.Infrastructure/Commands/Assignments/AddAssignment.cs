using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
	public class AddAssignment : IGroupIdentifier
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		[JsonIgnore]
		public Guid GroupId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Semester { get; set; }
		public DateTime? Deadline { get; set; }
		public Guid CourseId { get; set; }
	}
}