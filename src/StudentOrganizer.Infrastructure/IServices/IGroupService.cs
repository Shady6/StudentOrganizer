using StudentOrganizer.Infrastructure.Commands.Group;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IGroupService
	{
		Task CreateAsync(CreateGroup command);
	}
}