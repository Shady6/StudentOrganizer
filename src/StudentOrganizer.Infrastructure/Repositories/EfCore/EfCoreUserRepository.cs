using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Contexts;

namespace StudentOrganizer.Infrastructure.Repositories.EfCore
{

	public class EfCoreUserRepository : EfCoreRepository<User>, IUserRepository
	{		
		public EfCoreUserRepository(EfCoreDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<User> GetAsync(string mail)
		{
			return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == mail);
		}

		public IQueryable<User> GetSuggestedAsync(string searchLetters, Guid groupId)
		{
			return _dbContext.Users.Where(u =>
			!u.Groups.Any(g => g.Id == groupId) &&
			(EF.Functions.ILike(u.Email, $"%{searchLetters}%") ||
			EF.Functions.ILike(u.FirstName + " " + u.LastName, $"%{searchLetters}%")));
		}

		public async Task<User> GetWithAdministratedGroupsAsync(Guid userId)
		{
			return await _dbContext.Users.Where(u => u.Id == userId)
				.Include(u => u.AdministratedGroups)
				.FirstOrDefaultAsync();
		}

		public async Task<List<User>> GetUsersByEmailsAsync(List<string> Emails)
		{
			return await _dbContext.Users.Where(u => Emails.Contains(u.Email))
                .ToListAsync();
		}
	}
}