using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Schedules;
using StudentOrganizer.Infrastructure.Commands.Teams;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Api.Controllers
{
	[Route("groups/{groupId}/[controller]")]
	[Authorize]
	public class TeamsController : ApiControllerBase
	{
		private readonly ITeamService _teamService;
		private readonly IScheduleService _scheduleService;

		public TeamsController(ITeamService courseService, IScheduleService scheduleService)
		{
			_teamService = courseService;
			_scheduleService = scheduleService;
		}

		[HttpPost("{teamName}/schedules")]
		public async Task<ActionResult> AddSchedule(Guid groupId, string teamName, [FromBody] AddSchedule command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			command.TeamName = teamName;
			await _scheduleService.AddTeamSchedule(command);

			return Ok();
		}

		[HttpDelete("{teamName}/schedules/{semester}")]
		public async Task<ActionResult> DeleteSchedule(Guid groupId, string teamName, int semester)
		{
			var command = new DeleteSchedule
			{
				GroupId = groupId,
				UserId = User.GetUserId(),
				TeamName = teamName,
				Semester = semester
			};
			await _scheduleService.DeleteTeamSchedule(command);

			return Ok();
		}

		[HttpPut("{teamName}/schedules")]
		public async Task<ActionResult> UpdateSchedule(Guid groupId, string teamName, [FromBody] UpdateSchedule command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			command.TeamName = teamName;
			await _scheduleService.UpdateTeamSchedule(command);

			return Ok();
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
	}
}