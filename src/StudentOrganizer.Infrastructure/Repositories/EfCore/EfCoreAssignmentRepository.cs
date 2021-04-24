using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Contexts;

namespace StudentOrganizer.Infrastructure.Repositories.EfCore
{
	public class EfCoreAssignmentRepository : EfCoreRepository<Assignment>, IAssignmentRepository
	{		
		public EfCoreAssignmentRepository(EfCoreDbContext dbContext) : base(dbContext)
		{
		}

		public Task<IEnumerable<Assignment>> BrowseAsync(string name = "")
		{
			throw new System.NotImplementedException();
		}

		public Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Assignment assignment)
		{
			throw new NotImplementedException();
		}
	}
}