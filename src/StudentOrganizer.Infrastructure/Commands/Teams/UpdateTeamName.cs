using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Teams
{
	public class UpdateTeamName
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		[JsonIgnore]
		public Guid GroupId { get; set; }
		public string TeamName { get; set; }
		public string NewTeamName { get; set; }
	}
}