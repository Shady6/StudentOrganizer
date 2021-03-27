namespace StudentOrganizer.Infrastructure.User.Commands
{
	public class LoginUserCommand : CreateCommandBase
	{				
		public string Email { get; set; }
		public string Password { get; set; }		
	}
}