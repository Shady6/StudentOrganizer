using System;
using System.Collections.Generic;
using System.Linq;
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

		Task<List<User>> GetUsersByEmailsAsync(List<string> Emails);		
		IQueryable<User> GetSuggestedAsync(string searchLetters, Guid groupId);
		Task<User> GetWithAdministratedAndModeratedGroups(Guid userId);
		Task<User> GetWithAllGroupsAsync(Guid userId);
		Task<User> GetWithTeamsAsync(Guid userId);		
		Task<User> GetWithGroupTeamsAndStudents(string email);
	}
}