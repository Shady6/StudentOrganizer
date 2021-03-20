using MediatR;
using StudentOrganizer.Infrastructure.User.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.User.Handlers
{
	public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
	{
		public Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}
}