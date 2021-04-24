using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Teams;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Api.Controllers
{
	[Route("groups/{groupId}/[controller]")]
	[Authorize]
	public class TeamsController : ApiControllerBase
	{
		private readonly ITeamService _teamService;

		public TeamsController(ITeamService courseService)
		{
			_teamService = courseService;
		}

		[HttpPost]
		public async Task<ActionResult> AddTeams(Guid groupId, [FromBody] AddTeams command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			await _teamService.AddTeams(command);

			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteTeam(Guid groupId, [FromBody] DeleteTeam command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			await _teamService.DeleteTeam(command);

			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdateTeamName(Guid groupId, [FromBody] UpdateTeamName command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			await _teamService.UpdateTeamName(command);

			return Ok();
		}
	}
}