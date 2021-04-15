﻿using System;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Repositories
{
	public interface IUserRepository : IRepository
	{
		Task AddAsync(User user);

		Task<User> GetAsync(Guid id);

		Task<User> GetAsync(string mail);

		Task DeleteAsync(Guid id);

		Task UpdateAsync(User user);		
	}
}