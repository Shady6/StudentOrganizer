using MediatR;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.User.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Handlers.User
{
	public class LoginUserHandler : IRequestHandler<LoginUserCommand>
	{
		private readonly IUserService _userService;

		public LoginUserHandler(IUserService userService)
		{
			_userService = userService;
		}

		public Task<Unit> Handle(LoginUserCommand command, CancellationToken cancellationToken)
		{
			_userService.LoginAsync(command.Id, command.Email, command.Password);
			return Unit.Task;
		}
	}
}
