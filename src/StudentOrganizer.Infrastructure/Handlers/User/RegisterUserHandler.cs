using MediatR;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.User.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.User.Handlers
{
	public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
	{
		private readonly IUserService _userService;

		public RegisterUserHandler(IUserService userService)
		{
			_userService = userService;
		}

		public Task<Unit> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
		{
			_userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password,
				command.FirstName, command.LastName, command.Role);
			return Unit.Task;
		}
	}
}