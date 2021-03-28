using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Core.Repositories
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group);        
        Task<Group> GetAsync(Guid id);
        Task<Group> GetAsync(string name);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Group group);
    }
}
