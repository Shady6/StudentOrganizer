using MediatR;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
	public class RegisterUserCommand : IRequest
	{
		public RoleDto Role { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}