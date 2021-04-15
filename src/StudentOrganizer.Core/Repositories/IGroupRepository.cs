using System;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Repositories
{
	public interface IGroupRepository
	{
		Task AddAsync(Group group);

		Task<Group> GetAsync(Guid id);

		Task<Group> GetAsync(string name);

		Task DeleteAsync(Guid id);

		Task UpdateAsync(Group group);		
	}
}