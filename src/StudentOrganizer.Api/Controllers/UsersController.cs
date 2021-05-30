using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Users.Commands;

namespace StudentOrganizer.Api.Controllers
{
	[Authorize]
	[Route("")]
	public class UsersController : ControllerBase
	{
		protected readonly IMediator _mediator;
		private readonly IMemoryCache _memoryCache;
		private readonly IUserService _userService;
		private readonly IWebHostEnvironment _hostEnvironment;

		public UsersController(IMediator mediatr, IMemoryCache memoryCache, IUserService userService, IWebHostEnvironment hostEnvironment)
		{
			_mediator = mediatr;
			_memoryCache = memoryCache;
			_userService = userService;
			_hostEnvironment = hostEnvironment;
		}

		/// <summary>
		/// Only send ImageFile, don't send other values they will be overwritten
		/// </summary>		
		[HttpPost("users/avatar")]
		public async Task<ActionResult> SetAvatar([FromForm] SetAvatar command)
		{
			command.UserId = User.GetUserId();
			command.ImageBaseHttpPath = string.Format("{0}://{1}{2}/UserAvatars", Request.Scheme, Request.Host, Request.PathBase);
			command.ImagesFolderPath = Path.Combine(_hostEnvironment.ContentRootPath, "UserAvatars");

			await _userService.SetAvatar(command);
			return Ok();
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

		[AllowAnonymous]
		[HttpPost("users/register")]
		public async Task<ActionResult> Register([FromBody] RegisterUser command)
		{
			await _mediator.Send(command);
			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("users/login")]
		public async Task<ActionResult<JwtDto>> Login([FromBody] LoginUserCommand command)
		{
			var result = await _userService.LoginAsync(command);			
			return Ok(result);
		}
	}
}