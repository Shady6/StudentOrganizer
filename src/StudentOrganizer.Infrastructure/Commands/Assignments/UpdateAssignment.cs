using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
	public class UpdateAssignment : IGroupIdentifier
	{
		[JsonIgnore]
		public Guid AssignmentId { get; set; }
		[JsonIgnore]
		public Guid UserId { get; set; }
		[JsonIgnore]
		public Guid GroupId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Semester { get; set; }
		public DateTime? Deadline { get; set; }
		public Guid? CourseId { get; set; }
	}
}