using StudentOrganizer.Infrastructure.Commands;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
	public class LoginUserCommand
	{				
		public string Email { get; set; }
		public string Password { get; set; }		
	}
}