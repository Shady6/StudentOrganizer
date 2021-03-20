using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Infrastructure.User.Commands;
using System.Threading.Tasks;

namespace StudentOrganizer.Api.Controllers
{
	public class UserController : ControllerBase
	{
		protected readonly IMediator _mediator;

		public UserController(IMediator mediatr)
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