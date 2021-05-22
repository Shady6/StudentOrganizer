using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentOrganizer.Infrastructure.Commands.Assignments;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IAssignmentService : IService
	{
		Task AddAssignment(AddAssignment command);
		Task DeleteAssignment(DeleteAssignment command);
		Task UpdateAssignment(UpdateAssignment command);
	}
}