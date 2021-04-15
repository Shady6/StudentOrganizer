using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Group
{
	public class GetMyGroup
	{
		public Guid GroupId { get; set; }

		[JsonIgnore]
		public Guid UserId { get; set; }
	}
}