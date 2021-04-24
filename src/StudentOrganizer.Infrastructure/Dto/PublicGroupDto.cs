using System;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class PublicGroupDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool UserBelongsToGroup { get; set; }
	}
}