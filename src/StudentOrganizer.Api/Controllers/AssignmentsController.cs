using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Infrastructure.Commands.Assignments;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Api.Controllers
{
	[Route("[controller]")]	
	public class AssignmentsController : ApiControllerBase
	{
		private readonly IAssignmentService _assignmentService;

		public AssignmentsController(IAssignmentService assignmentService)
		{
			_assignmentService = assignmentService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string name)
		{
			var assignments = await _assignmentService.BrowseAsync(name);
			return Ok(assignments);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateAssignment command)
		{
			Guid id = Guid.NewGuid();
			await _assignmentService.CreateAsync(id, command.Name, command.Description, command.Semester, command.Deadline);
			return Created($"/assignments/{id}", null);
		}
	}
}