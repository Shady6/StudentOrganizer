using System;
using Newtonsoft.Json;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
	public class PromoteUserToModerator
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }

		public string UserEmailToPromote{ get; set; }
	}
}