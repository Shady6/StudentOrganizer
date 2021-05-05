using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Groups;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Api.Controllers
{
	[Route("[controller]")]
	[Authorize]
	public class GroupsController : ApiControllerBase
	{
		private readonly IGroupService _groupService;

		public GroupsController(IGroupService groupService)
		{
			_groupService = groupService;
		}

		[HttpPost]
		public async Task<ActionResult> CreateGroup([FromBody] CreateGroup command)
		{
			command.UserId = User.GetUserId();
            await Mediator.Send(command);
			return Ok(new { Id = command.Id });
			//return CreatedAtAction("GetGroup", new { Id = command.Id });
		}

		[HttpGet("attended/{groupId}")]
		public async Task<ActionResult<List<GroupDto>>> GetMyGroup([FromRoute] GetMyGroup command)
		{
			command.UserId = User.GetUserId();
			return Ok(await _groupService.GetMyGroup(command));
		}

		[HttpGet("attended")]
		public ActionResult<List<SmallGroupDto>> GetMyGroups()
		{
			var command = new GetMyGroups
			{
				UserId = User.GetUserId()
			};
			return Ok(_groupService.GetMyGroups(command));
		}

		[HttpGet()]
		[AllowAnonymous]
		public ActionResult<List<PublicGroupDto>> GetAllGroups()
		{
			var command = new GetPublicGroups();
			try
			{
				command.UserId = User.GetUserId();
			}
			catch (Exception)
			{
				command.UserId = new Guid();
			}
			return Ok(_groupService.GetAllGroups(command));
		}
	}
}
