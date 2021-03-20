using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task AddAsync(Team user);
        Task<IEnumerable<Team>> BrowseAsync(string mail = "");
        Task<Team> GetAsync(Guid id);
        Task<Team> GetAsync(string mail);
        Task DeleteAsync(Guid id);
        Task UpdateAsync();
    }
}
