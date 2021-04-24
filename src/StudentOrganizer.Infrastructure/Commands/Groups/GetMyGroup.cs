using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
	public class GetMyGroup
	{
		public Guid GroupId { get; set; }

		[JsonIgnore]
		public Guid UserId { get; set; }
	}
}