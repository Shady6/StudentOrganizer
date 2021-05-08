using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Contexts;

namespace StudentOrganizer.Infrastructure.Repositories.EfCore
{
	public class EfCoreGroupRepository : EfCoreRepository<Group>, IGroupRepository
	{
		public EfCoreGroupRepository(EfCoreDbContext dbContext) : base(dbContext)
		{
		}

		public async override Task AddAsync(Group entity)
		{
			await _dbContext.AddAsync(entity);
		}

		public async Task<Group> GetAsync(string name)
		{
			return await _dbContext.Group.FirstOrDefaultAsync(g => g.Name == name);
		}

		public async Task<Group> GetWithStudents(Guid GroupId)
		{
			return await _dbContext.Group.Where(g => g.Id == GroupId)
				.Include(g => g.Students)
				.FirstOrDefaultAsync();
		}

		public async Task<Group> GetWithCoursesAsync(Guid id)
		{
			return await _dbContext.Group.Where(g => g.Id == id)
				.Include(g => g.Courses)
				.FirstOrDefaultAsync();
		}

		public async Task<Group> GetWithTeamsAsync(Guid id)
		{
			return await _dbContext.Group.Where(g => g.Id == id)
				.Include(g => g.Teams)
				.FirstOrDefaultAsync();
		}

		public async Task<Group> GetWithTeamScheduleAndCourses(Guid id, string teamName)
		{
			return await _dbContext.Group.Where(g => g.Id == id &&
			g.Teams.Any(t => t.Name == teamName))
				.Include(g => g.Courses)
				.Include(g => g.Teams)
				.ThenInclude(t => t.Schedules)
				.ThenInclude(s => s.ScheduledCourses)
				.ThenInclude(sc => sc.Course)
				.FirstOrDefaultAsync();
		}

		public async Task<Group> GetWholeGroupAsync(Guid id)
		{
			return await _dbContext.Group.Where(g => g.Id == id)
				.Include(g => g.Students)
				.Include(g => g.Administrators)
				.Include(g => g.Schedules).ThenInclude(s => s.ScheduledCourses).ThenInclude(c => c.Course)
				.Include(g => g.Teams).ThenInclude(t => t.Schedules).ThenInclude(t => t.ScheduledCourses).ThenInclude(c => c.Course)
				.Include(g => g.Teams).ThenInclude(t => t.Students)
				.Include(g => g.Teams).ThenInclude(t => t.Assignmets).ThenInclude(a => a.Course)
				.Include(g => g.Assignmets).ThenInclude(a => a.Course)
				.Include(g => g.Courses)
				.FirstOrDefaultAsync();
		}

		public IQueryable<Group> GetWholeGroupsAsync(Guid userId)
		{
			return _dbContext.Group.Where(g => g.Students.Any(s => s.Id == userId))
				.Include(g => g.Students)
				.Include(g => g.Administrators)
				.Include(g => g.Schedules).ThenInclude(s => s.ScheduledCourses).ThenInclude(c => c.Course)
				.Include(g => g.Teams).ThenInclude(t => t.Schedules).ThenInclude(t => t.ScheduledCourses).ThenInclude(c => c.Course)
				.Include(g => g.Teams).ThenInclude(t => t.Students)
				.Include(g => g.Teams).ThenInclude(t => t.Assignmets).ThenInclude(a => a.Course)
				.Include(g => g.Assignmets).ThenInclude(a => a.Course)
				.Include(g => g.Courses)
				.AsQueryable();
		}

		public async Task<Group> GetWithAllUsers(Guid groupId)
		{
			return await _dbContext.Group.Where(g => g.Id == groupId)
				.Include(g => g.Students)
				.Include(g => g.Administrators)
				.Include(g => g.Moderators)
				.FirstOrDefaultAsync();
		}

		public IQueryable<Group> GetAll()
		{
			return _dbContext.Group;
		}		
	}
}