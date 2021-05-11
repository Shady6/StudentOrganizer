using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
	public class AddUsersToGroup
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }

		public List<string> Emails { get; set; }
	}
}