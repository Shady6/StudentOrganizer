using System.Threading.Tasks;
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

		public Task<Group> GetAsync(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}