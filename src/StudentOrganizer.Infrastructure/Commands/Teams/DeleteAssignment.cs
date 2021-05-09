using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Teams
{
	public class DeleteAssignment
	{
		[JsonIgnore]
		public Guid TeamId { get; set; }
		[JsonIgnore]
		public Guid AssignmentId { get; set; }
	}
}