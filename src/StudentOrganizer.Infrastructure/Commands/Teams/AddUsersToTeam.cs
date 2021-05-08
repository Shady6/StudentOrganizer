using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Teams
{
    public class AddUsersToTeam
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		[JsonIgnore]
		public Guid GroupId { get; set; }
		[JsonIgnore]
		public string TeamName { get; set; }
		public List<string> Emails { get; set; }
	}

}