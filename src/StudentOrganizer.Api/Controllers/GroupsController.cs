using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Infrastructure.Commands.Group;
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
			command.AuthorId = User.GetUserId();
			await _groupService.CreateAsync(command);
			return Ok(new { Id = command.Id });
			//return CreatedAtAction("GetGroup", new { Id = command.Id });
		}

		[HttpGet("attented/{groupId}")]
		public async Task GetMyGroup([FromRoute]GetMyGroup command)
		{
			command.UserId = User.GetUserId();
			return Ok(await _groupService.GetMyGroup(command));
		}

		[HttpGet("attented")]
		public async Task GetMyGroups()
		{
			var command = new GetMyGroups
			{
				UserId = User.GetUserId()
			};
			return Ok(await _groupService.GetMyGroups(command));

		}

		[HttpGet()]
		[AllowAnonymous]
		public async Task GetAllGroups()
		{
			return Ok(await _groupService.GetAllGroups());
		}
	}
}