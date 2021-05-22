using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Assignments;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Api.Controllers
{
	[Authorize]
	public class AssignmentsController : ApiControllerBase
	{
		private readonly IAssignmentService _assignmentService;

		public AssignmentsController(IAssignmentService assignmentService)
		{
			_assignmentService = assignmentService;
		}

		/// <remarks>
		/// If sending courseId don't send the semester because it will be overwritten by the semester of specified course.&#xA;&#xD;
		/// Otherwise if not sending courseId you should send the semester of the given assignment
		/// </remarks>		
		[HttpPost("groups/{groupId}/[controller]")]
		public async Task<ActionResult> AddAssignment(Guid groupId, [FromBody] AddAssignment command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			await _assignmentService.AddAssignment(command);

			return Ok();
		}

		/// <remarks>
		/// If sending courseId don't send the semester because it will be overwritten by the semester of specified course.&#xA;&#xD;
		/// Otherwise if not sending courseId you should send the semester of the given assignment
		/// </remarks>		
		[HttpPut("groups/{groupId}/[controller]/{assignmentId}")]
		public async Task<ActionResult> UpdateAssignment(Guid groupId, Guid assignmentId, [FromBody] UpdateAssignment command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			command.AssignmentId = assignmentId;
			await _assignmentService.UpdateAssignment(command);

			return Ok();
		}

		[HttpDelete("groups/{groupId}/[controller]/{assignmentId}")]
		public async Task<ActionResult> DeleteAssignment(Guid groupId, Guid assignmentId)
		{
			var command = new DeleteAssignment
			{
				UserId = User.GetUserId(),
				GroupId = groupId,
				AssignmentId = assignmentId
			};
			await _assignmentService.DeleteAssignment(command);

			return Ok();
		}

		/// <remarks>
		/// If sending courseId don't send the semester because it will be overwritten by the semester of specified course.&#xA;&#xD;
		/// Otherwise if not sending courseId you should send the semester of the given assignment
		/// </remarks>		
		[HttpPost("groups/{groupId}/teams/{teamName}/[controller]")]
		public async Task<ActionResult> AddAssignmentToTeam(Guid groupId, string teamName, [FromBody] AddAssignmentToTeam command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			command.TeamName = teamName;
			await _assignmentService.AddAssignment(command);

			return Ok();
		}

		/// <remarks>
		/// If sending courseId don't send the semester because it will be overwritten by the semester of specified course.&#xA;&#xD;
		/// Otherwise if not sending courseId you should send the semester of the given assignment
		/// </remarks>		
		[HttpPut("groups/{groupId}/teams/{teamName}/[controller]/{assignmentId}")]
		public async Task<ActionResult> UpdateAssignmentInTeam(Guid groupId, string teamName, Guid assignmentId, [FromBody] UpdateAssignmentInTeam command)
		{
			command.UserId = User.GetUserId();
			command.GroupId = groupId;
			command.AssignmentId = assignmentId;
			command.TeamName = teamName;
			await _assignmentService.UpdateAssignment(command);

			return Ok();
		}

		[HttpDelete("groups/{groupId}/teams/{teamName}/[controller]/{assignmentId}")]
		public async Task<ActionResult> DeleteAssignmentFromTeam(Guid groupId, Guid assignmentId, string teamName)
		{
			var command = new DeleteAssignmentFromTeam
			{
				UserId = User.GetUserId(),
				GroupId = groupId,
				AssignmentId = assignmentId,
				TeamName = teamName
			};
			await _assignmentService.DeleteAssignment(command);

			return Ok();
		}
	}
}