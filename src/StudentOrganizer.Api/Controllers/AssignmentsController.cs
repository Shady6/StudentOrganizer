using Microsoft.AspNetCore.Mvc;
using StudentOrganizer.Infrastructure.IServices;

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
	}
}