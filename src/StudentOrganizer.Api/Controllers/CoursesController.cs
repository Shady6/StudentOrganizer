using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Api.Extentions;
using StudentOrganizer.Infrastructure.Commands.Courses;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Api.Controllers
{
	[Route("groups/{groupId}/[controller]")]
	[Authorize]
	public class CoursesController : ApiControllerBase
	{
		private readonly ICourseService _courseService;

		public CoursesController(ICourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpPost]
		public async Task<ActionResult> AddCourses(Guid groupId, [FromBody] AddCourses command)
		{
			command.GroupId = groupId;
			command.UserId = User.GetUserId();
			await _courseService.AddCourses(command);

			return Ok();
		}

		[HttpDelete("{courseId}")]
		public async Task<ActionResult> DeleteCourse(Guid groupId, Guid courseId)
		{
			var command = new DeleteCourse
			{
				GroupId = groupId,
				UserId = User.GetUserId(),
				CourseId = courseId
			};
			await _courseService.DeleteCourse(command);

			return Ok();
		}

		[HttpPut("{courseId}")]
		public async Task<ActionResult> UpdateCourse(Guid groupId, Guid courseId, [FromBody] UpdateCourse command)
		{
			command.GroupId = groupId;
			command.Course.Id = courseId;
			command.UserId = User.GetUserId();
			await _courseService.UpdateCourse(command);

			return Ok();
		}
	}
}