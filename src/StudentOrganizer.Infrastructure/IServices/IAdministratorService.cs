using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IAdministratorService
	{
		Task<bool> IsAdministratorInAnyGroup(Guid userId);
		Task ValidateAdministrativePrivileges(Guid userId, Guid groupId);
	}
}