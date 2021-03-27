using StudentOrganizer.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
    public interface IAssignmentService : IService
    {
        Task CreateAsync(Guid id, string name, string description, int semester, DateTime deadline);
        Task<AssignmentDto> GetAsync(Guid id);
        Task<IEnumerable<AssignmentDto>> BrowseAsync(string name="");
    }
}
