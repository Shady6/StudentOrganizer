using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Core.Repositories
{
    public interface ILocationRepository
    {
        Task AddAsync(Location location);
        Task<IEnumerable<Location>> BrowseAsync(string room = "");
        Task<Location> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync();
    }
}
