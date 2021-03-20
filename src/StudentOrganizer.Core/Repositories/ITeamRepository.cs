using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Core.Repositories
{
    public interface ITeamRepository : IRepository
    {
        Task AddAsync(Team team);
        Task<IEnumerable<Team>> BrowseAsync(string name = "");
        Task<Team> GetAsync(Guid id);
        Task<Team> GetAsync(string name);
        Task DeleteAsync(Guid id);
        Task UpdateAsync();
    }
}
