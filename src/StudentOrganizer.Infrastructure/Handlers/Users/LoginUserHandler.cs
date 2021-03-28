using MediatR;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Users.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Handlers.User
{
	public class LoginUserHandler : IRequestHandler<LoginUser>
	{
		private readonly IUserService _userService;

		public LoginUserHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Unit> Handle(LoginUser command, CancellationToken cancellationToken)
		{
			await _userService.LoginAsync(command.Id, command.Email, command.Password);
			return Unit.Value;
		}
	}
}