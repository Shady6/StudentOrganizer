using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Infrastructure.Users.Commands;
using System.Threading.Tasks;

namespace StudentOrganizer.Api.Controllers
{
	public class UsersController : ControllerBase
	{
		protected readonly IMediator _mediator;

		public UsersController(IMediator mediatr)
		{
			_mediator = mediatr;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Post([FromBody] RegisterUserCommand command)
		{
			await _mediator.Send(command);
			return Created("/users", null);
		}
	}
}