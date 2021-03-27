using MediatR;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Users.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Users.Handlers
{
	public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
	{
		private readonly IUserService _userService;

		public RegisterUserHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Unit> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
		{
			await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password,
				command.FirstName, command.LastName, command.Role);
			return Unit.Value;
		}
	}
}