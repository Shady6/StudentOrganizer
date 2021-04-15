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

		public async Task<Group> GetAsync(string name)
		{
			return await _dbContext.Group.FirstOrDefaultAsync(g => g.Name == name);
		}
	}
}