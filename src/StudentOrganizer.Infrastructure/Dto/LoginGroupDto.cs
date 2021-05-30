using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class LoginGroupDto
	{
		public string Name { get; set; }
		public List<string> UserTeams { get; set; }
		public List<string> Teams { get; set; }
	}

}