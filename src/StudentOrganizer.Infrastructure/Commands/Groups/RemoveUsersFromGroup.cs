using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
	public class RemoveUsersFromGroup
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }

		[JsonIgnore]
		public RemoveFromGroupRoleDto Role { get; set; }

		public List<string> Emails { get; set; }
	}
}