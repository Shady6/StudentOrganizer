using System;
using System.Linq;
using System.Threading.Tasks;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Infrastructure.Services
{
	public class AdministratorService : IAdministratorService
	{
		private readonly IUserRepository _userRepository;

		public AdministratorService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task ValidateAdministrativePrivileges(Guid userId, Guid groupId)
		{
			var user = await _userRepository.GetWithAdministratedGroupsAsync(userId);
			if (!user.AdministratedGroups.Any(g => g.Id == groupId))
				throw new AppException("You're not administrator of this group.", AppErrorCode.CANT_DO_THAT);
		}

		public async Task<bool> IsAdministratorInAnyGroup(Guid userId)
		{
			var user = await _userRepository.GetWithAdministratedGroupsAsync(userId);
			return user.AdministratedGroups.Count() > 0;
		}
	}
}