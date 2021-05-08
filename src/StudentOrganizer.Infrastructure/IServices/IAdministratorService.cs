using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IAdministratorService
	{
		Task<bool> IsAdministratorInAnyGroup(Guid userId);
		Task ValidateAtLeastAdministrator(Guid userId, Guid groupId);
		Task ValidateAtLeastModerator(Guid userId, Guid groupId);
	}
}