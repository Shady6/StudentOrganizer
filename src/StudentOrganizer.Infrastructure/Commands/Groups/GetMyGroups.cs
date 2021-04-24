using System;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
	public class GetMyGroups
	{						
		public Guid UserId { get; set; }
	}

	public class GetPublicGroups
	{
		public Guid UserId { get; set; }
	}
}