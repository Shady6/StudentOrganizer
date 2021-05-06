using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Repositories
{
	public interface IUserRepository : IRepository
	{
		Task AddAsync(User user);

		Task<User> GetAsync(Guid id);

		Task<User> GetAsync(string mail);

		void Delete(Guid id);

		void Update(User user);

		Task SaveChangesAsync();
		Task<User> GetWithAdministratedGroupsAsync(Guid userId);
        Task<List<User>> GetUsersAsync(List<string> Emails);
    }
}