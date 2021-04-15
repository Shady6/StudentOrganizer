using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Infrastructure.Commands.Group;
using StudentOrganizer.Infrastructure.Extentions;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

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

		//[HttpGet]
		//[AllowAnonymous]
		//public void GetAllGroups()
		//{
		//}

		[HttpPost]
		public async Task<ActionResult> CreateGroup([FromBody] CreateGroup command)
		{
			command.AuthorId = User.GetUserId();
			await _groupService.CreateAsync(command);
			return CreatedAtAction("GetGroup", new { Id = command.Id });
		}

		//[HttpGet]
		//public void GetGroup()
		//{
		//}

		//[HttpPut]
		//public void UpdateGroupName()
		//{
		//}

		//[HttpDelete]
		//public void DeleteGroup()
		//{
		//}
	}
}