﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Groups;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Users.Commands;

namespace StudentOrganizer.Api.Controllers
{
	[Route("[controller]")]
	[Authorize]
	public class GroupsController : ApiControllerBase
	{
		private readonly IGroupService _groupService;
		private readonly IUserService _userService;

		public GroupsController(IGroupService groupService, IUserService userService)
		{
			_groupService = groupService;
			_userService = userService;
		}

		[HttpPost]
		public async Task<ActionResult> CreateGroup([FromBody] CreateGroup command)
		{
			command.UserId = User.GetUserId();
			await _groupService.CreateAsync(command);
			return Ok(new { command.Id });
		}

		[HttpGet("attended/{groupId}")]
		public async Task<ActionResult<List<GroupDto>>> GetMyGroup([FromRoute] GetMyGroup command)
		{
			command.UserId = User.GetUserId();
			return Ok(await _groupService.GetMyGroup(command));
		}

		[HttpGet("attended/partial")]
		public ActionResult<List<SmallGroupDto>> GetMyGroupsPartial()
		{
			var command = new GetMyGroups
			{
				UserId = User.GetUserId()
			};
			return Ok(_groupService.GetMyGroups(command));
		}

		[HttpGet("attended/full")]
		public ActionResult<List<GroupDto>> GetMyGroupsFull()
		{
			var command = new GetMyGroups
			{
				UserId = User.GetUserId()
			};
			return Ok(_groupService.GetMyGroupsFull(command));
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

		[HttpPut("{groupId}")]
		public async Task<ActionResult> EditGroupName(Guid groupId, [FromBody] EditGroupName command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			await _groupService.EditGroupName(command);

			return Ok();
		}

		[HttpDelete("{groupId}")]
		public async Task<ActionResult> DeleteGroup(Guid groupId)
		{
			var command = new DeleteGroup
			{
				UserId = User.GetUserId(),
				GroupId = groupId
			};

			await _groupService.DeleteGroup(command);
			return Ok();
		}

		[HttpGet("{groupId}/users")]
		public async Task<ActionResult<List<DisplayUserDto>>> GetSuggestedUsers(Guid groupId, [FromQuery] GetSuggestedUsers command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			var data = await _userService.GetSuggestedUsers(command);
			return Ok(data);
		}

		[HttpPost("{groupId}/users")]
		public async Task<ActionResult> AddUsersToGroup(Guid groupId, [FromBody] AddUsersToGroup command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			await _groupService.AddUsersToGroup(command);
			return Ok();
		}

		[HttpDelete("{groupId}/users")]
		public async Task<ActionResult> RemoveUsersFromGroup(Guid groupId, [FromQuery] RemoveFromGroupRoleDto roleToRemoveFrom, [FromBody] RemoveUsersFromGroup command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			command.Role = roleToRemoveFrom;
			await _groupService.RemoveUsersFromGroup(command);
			return Ok();
		}
		
		[HttpPost("{groupId}/users/promoteToMod")]
		public async Task<ActionResult> PromoteUserToModerator(Guid groupId, [FromBody] PromoteUserToModerator command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			await _groupService.PromoteUserToModerator(command);
			return Ok();
		}
	}
}