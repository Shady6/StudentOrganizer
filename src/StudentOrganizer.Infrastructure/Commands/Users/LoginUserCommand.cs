namespace StudentOrganizer.Infrastructure.Users.Commands
{
	public class LoginUserCommand : CreateCommandBase
	{				
		public string Email { get; set; }
		public string Password { get; set; }		
	}
}