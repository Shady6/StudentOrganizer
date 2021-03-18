using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Core.Repositories
{
    public interface IAddressRepository
    {
        Task AddAsync(Address address);
        Task<IEnumerable<Address>> BrowseStreetsAsync(string streetName = "");
        Task<IEnumerable<Address>> BrowseCitiesAsync(string city = "");
        Task<Address> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync();
    }
}
