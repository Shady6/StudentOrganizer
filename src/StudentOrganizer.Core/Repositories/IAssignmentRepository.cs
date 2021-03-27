using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Core.Repositories
{
    public interface IAssignmentRepository
    {
        Task AddAsync(Assignment assignment);
        Task<IEnumerable<Assignment>> BrowseAsync(string name = "");
        Task<Assignment> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Assignment assignment);
    }
}
