using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class LoginUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public List<LoginGroupDto> Groups { get; set; }
	}
}