using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{				
		public async Task AddAsync(Core.Models.User user)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Core.Models.User>> BrowseAsync(string email = "")
		{
			throw new NotImplementedException();					
		}

		public async Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<Core.Models.User> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<Core.Models.User> GetAsync(string email)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync()
		{
			throw new NotImplementedException();
		}

		public async Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}
	}
}
