using System;
using System.Linq;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Repositories
{
	public interface IGroupRepository
	{
		Task AddAsync(Group group);

		Task<Group> GetAsync(Guid id);

		Task<Group> GetAsync(string name);

		void Delete(Guid id);

		void Update(Group group);

		IQueryable<Group> GetAll();

		Task<Group> GetWholeGroupAsync(Guid id);

		Task SaveChangesAsync();
		Task<Group> GetWithCoursesAsync(Guid id);
		Task<Group> GetWithTeamsAsync(Guid id);
		Task<Group> GetWithTeamScheduleAndCourses(Guid id, string teamName);
	}
}