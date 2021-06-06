using System;
using Newtonsoft.Json;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
	public class DeleteAssignment : IGroupIdentifier
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		[JsonIgnore]
		public Guid GroupId { get; set; }
		[JsonIgnore]
		public Guid AssignmentId { get; set; }
	}
}