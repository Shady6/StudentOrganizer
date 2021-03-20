using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace StudentOrganizer.Core.Repositories
{
    public interface ICourseRepository
    {
        Task AddAsync(Course course);
        Task<IEnumerable<Course>> BrowseAsync(string name = "");
        Task<Course> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync();
    }
}

