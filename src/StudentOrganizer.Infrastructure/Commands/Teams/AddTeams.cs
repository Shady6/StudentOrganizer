using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudentOrganizer.Infrastructure.Commands.Teams
{
	public class AddTeams
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		[JsonIgnore]
		public Guid GroupId { get; set; }
		public List<string> TeamNames { get; set; }
	}
}