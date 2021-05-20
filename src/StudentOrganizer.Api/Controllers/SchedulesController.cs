using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Schedules;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Api.Controllers
{
	[Route("groups/{groupId}/teams")]
	[Authorize]
	public class SchedulesController : ApiControllerBase
	{
		private readonly IScheduleService _scheduleService;

		public SchedulesController(IScheduleService scheduleService)
		{
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

		[HttpPut("{teamName}/schedules")]
		public async Task<ActionResult> UpdateSchedule(Guid groupId, string teamName, [FromBody] UpdateSchedule command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			command.TeamName = teamName;
			await _scheduleService.UpdateTeamSchedule(command);

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
	}
}