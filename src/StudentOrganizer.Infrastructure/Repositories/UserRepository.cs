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
		public async Task AddAsync(User user)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<User>> BrowseAsync(string email = "")
		{
			throw new NotImplementedException();					
		}

		public async Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<User> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<User> GetAsync(string email)
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
