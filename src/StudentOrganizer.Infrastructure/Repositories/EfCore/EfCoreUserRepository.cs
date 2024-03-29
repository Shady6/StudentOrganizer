﻿using System;
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

		public async Task<User> GetWithGroupTeamsAndStudents(string email)
		{
			return await _dbContext.Users.Where(u => u.Email == email)
				.Include(u => u.Groups).ThenInclude(g => g.Administrators)
				.Include(u => u.Groups).ThenInclude(g => g.Moderators)
				.Include(u => u.Groups).ThenInclude(g => g.Students)
				.Include(u => u.Groups)
				.ThenInclude(g => g.Teams)
				.ThenInclude(t => t.Students)
				.FirstOrDefaultAsync();
		}

		public async Task<User> GetWithTeamsAsync(Guid userId)
		{
			return await _dbContext.Users.Where(u => u.Id == userId)
				.Include(u => u.Teams)
				.FirstOrDefaultAsync();
		}

		public async Task<User> GetWithAllGroupsAsync(Guid userId)
		{
			return await _dbContext.Users.Where(u => u.Id == userId)
				.Include(u => u.Groups)
				.Include(u => u.AdministratedGroups).ThenInclude(ag => ag.Administrators)
				.Include(u => u.ModeratedGroups)
				.FirstOrDefaultAsync();
		}

		public async Task<User> GetWithAdministratedAndModeratedGroups(Guid userId)
		{
			return await _dbContext.Users.Where(u => u.Id == userId)
				.Include(u => u.AdministratedGroups)
				.Include(u => u.ModeratedGroups)
				.FirstOrDefaultAsync();
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