using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Users.Commands;

namespace StudentOrganizer.Api.Controllers
{
	[Route("")]
	public class UsersController : ControllerBase
	{
		protected readonly IMediator _mediator;
		private readonly IMemoryCache _memoryCache;
		private readonly IUserService _userService;

		public UsersController(IMediator mediatr, IMemoryCache memoryCache, IUserService userService)
		{
			_mediator = mediatr;
			_memoryCache = memoryCache;
			_userService = userService;
		}
		
		[HttpDelete("groups/{groupId}/users/leave")]
		public async Task<ActionResult> LeaveGroup(Guid groupId, [FromQuery] GroupToLeave groupToLeave)
		{
			var command = new LeaveGroup
			{
				UserId = User.GetUserId(),
				GroupId = groupId,
				GroupToLeave = groupToLeave
			};
			await _userService.LeaveGroup(command);
			return Ok();
		}
		
		[HttpDelete("groups/{groupId}/teams/{teamName}/users/leave")]
		public async Task<ActionResult> LeaveTeam(Guid groupId, string teamName)
		{
			var command = new LeaveTeam
			{
				UserId = User.GetUserId(),
				GroupId = groupId,
				TeamName = teamName
			};
			await _userService.LeaveTeam(command);
			return Ok();
		}

		[HttpPost("users/register")]
		public async Task<ActionResult> Register([FromBody] RegisterUser command)
		{
			await _mediator.Send(command);
			return Ok();
		}

		[HttpPost("users/login")]
		public async Task<ActionResult<JwtDto>> Login([FromBody] LoginUser command)
		{
			await _mediator.Send(command);
			var jwt = _memoryCache.Get<JwtDto>(command.Id);
			return Ok(jwt);
		}

		[HttpDelete("users")]
		[Authorize]
		public async Task<ActionResult> DeleteUser()
		{
			var command = new DeleteUser
			{
				UserId = User.GetUserId()
			};

			await _userService.DeleteUser(command);
			return Ok();
		}
	}
}