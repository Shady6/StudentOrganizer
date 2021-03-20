using Microsoft.EntityFrameworkCore;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<Core.Models.User> _users;

		public UserRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_users = dbContext.Users;
		}

		public async Task AddAsync(Core.Models.User user)
		{
			await _users.AddAsync(user);			
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
			return await _users.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<Core.Models.User> GetAsync(string email)
		{
			return await _users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task UpdateAsync()
		{
			throw new NotImplementedException();
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
