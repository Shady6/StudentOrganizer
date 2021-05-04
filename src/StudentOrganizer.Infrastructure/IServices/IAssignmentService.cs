using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IAssignmentService : IService
	{
		Task CreateAsync(Guid id, string name, string description, int semester, DateTime deadline);

		Task<AssignmentDto> GetAsync(Guid id);

		Task<IEnumerable<AssignmentDto>> BrowseAsync(string name = "");
	}
}