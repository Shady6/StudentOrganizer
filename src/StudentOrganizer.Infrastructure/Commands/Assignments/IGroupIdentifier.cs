using System;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
	public interface IGroupIdentifier
	{
		public Guid GroupId { get; set; }
	}
}