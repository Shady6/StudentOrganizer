using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Infrastructure.Contexts;

namespace StudentOrganizer.Infrastructure.Repositories.EfCore
{
	public abstract class EfCoreRepository<T> where T : Entity
	{
		protected readonly EfCoreDbContext _dbContext;

		public EfCoreRepository(EfCoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual async Task AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async virtual Task AddRangeAsync(T[] entities)
		{
			await _dbContext.Set<T>().AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();			
		}

		public async virtual Task DeleteAsync(Guid id)
		{
			_dbContext.Remove(_dbContext.Set<T>().Where(e => e.Id == id).FirstOrDefault());
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task<T> GetAsync(Guid id)
		{
			return await _dbContext
				.Set<T>()
				.FindAsync(id);
		}

		public virtual async Task<List<T>> GetAllAsync()
		{
			return await _dbContext
				.Set<T>()
				.ToListAsync();
		}

		public virtual async Task<T> GetByIdAsync(Guid id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public virtual async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
		{
			return await _dbContext
				.Set<T>()
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.AsNoTracking()
				.ToListAsync();
		}

		public async virtual Task UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).CurrentValues.SetValues(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}