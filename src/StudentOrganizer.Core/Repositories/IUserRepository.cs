﻿using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentOrganizer.Core.Repositories
{
	public interface IUserRepository : IRepository
	{
		Task AddAsync(User user);

		Task<IEnumerable<User>> BrowseAsync(string mail = "");

		Task<User> GetAsync(Guid id);

		Task<User> GetAsync(string mail);

		Task DeleteAsync(Guid id);

		Task UpdateAsync();

		Task SaveChangesAsync();
	}
}