using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Infrastructure.Commands.Group;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.Extentions;
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
			await _groupService.CreateAsync(command);
			return Ok(new { Id = command.Id });
			//return CreatedAtAction("GetGroup", new { Id = command.Id });
		}

		[HttpGet("attended/{groupId}")]
		public async Task<List<GroupDto>> GetMyGroup([FromRoute]GetMyGroup command)
		{
			command.UserId = User.GetUserId();
			throw new NotImplementedException();
			//return Ok(await _groupService.GetMyGroup(command));
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
			return Ok(_groupService.GetAllGroups());
		}
	}
}