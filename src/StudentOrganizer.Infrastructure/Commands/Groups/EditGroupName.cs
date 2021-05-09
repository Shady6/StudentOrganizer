using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
	public class EditGroupName
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }

		public string NewName { get; set; }
	}
}