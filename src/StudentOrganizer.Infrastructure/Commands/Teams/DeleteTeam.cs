using System;
using Newtonsoft.Json;

namespace StudentOrganizer.Infrastructure.Commands.Teams
{
	public class DeleteTeam
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		[JsonIgnore]
		public Guid GroupId { get; set; }
		[JsonIgnore]
		public string TeamName { get; set; }
	}
}