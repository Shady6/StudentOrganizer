using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Teams;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

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

		[HttpDelete("{teamName}")]
		public async Task<ActionResult> DeleteTeam(Guid groupId, string teamName)
		{
			var command = new DeleteTeam
			{
				GroupId = groupId,
				UserId = User.GetUserId(),
				TeamName = teamName
			};

			await _teamService.DeleteTeam(command);

			return Ok();
		}

		[HttpPut("{teamName}")]
		public async Task<ActionResult> UpdateTeamName(Guid groupId, string teamName, [FromBody] UpdateTeamName command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			command.TeamName = teamName;
			await _teamService.UpdateTeamName(command);

			return Ok();
		}

		[HttpPost("{teamName}/users")]
		public async Task<ActionResult> AddUsersToTeam(Guid groupId, string teamName, [FromBody] AddUsersToTeam command)
		{
			command.GroupId = groupId;
			command.TeamName = teamName;
			command.UserId = User.GetUserId();
			await _teamService.AddUsersToTeam(command);
			return Ok();
		}

		[HttpDelete("{teamName}/users")]
		public async Task<ActionResult> RemoveUsersFromTeam(Guid groupId, string teamName, [FromBody] RemoveUsersFromTeam command)
		{
			command.GroupId = groupId;
			command.TeamName = teamName;
			command.UserId = User.GetUserId();
			await _teamService.RemoveUsersFromTeam(command);
			return Ok();
		}
	}
}