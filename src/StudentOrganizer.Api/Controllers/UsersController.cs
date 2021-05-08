using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.Users.Commands;

namespace StudentOrganizer.Api.Controllers
{
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		protected readonly IMediator _mediator;
		private readonly IMemoryCache _memoryCache;

		public UsersController(IMediator mediatr, IMemoryCache memoryCache)
		{
			_mediator = mediatr;
			_memoryCache = memoryCache;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegisterUser command)
		{
			await _mediator.Send(command);
			return Ok();
		}

		[HttpPost("login")]
		public async Task<ActionResult<JwtDto>> Login([FromBody] LoginUser command)
		{
			await _mediator.Send(command);
			var jwt = _memoryCache.Get<JwtDto>(command.Id);
			return Ok(jwt);
		}
	}
}