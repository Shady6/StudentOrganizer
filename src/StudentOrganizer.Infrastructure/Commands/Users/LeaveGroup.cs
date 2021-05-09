using System;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
	public class LeaveGroup
	{
		public Guid UserId { get; set; }

		public Guid GroupId { get; set; }

		public GroupToLeave GroupToLeave { get; set; }
	}
}