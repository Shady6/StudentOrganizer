using System;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
	public class LeaveTeam
	{
		public Guid UserId { get; set; }

		public Guid GroupId { get; set; }

		public string TeamName { get; set; }
	}
}