using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Repositories;

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
	}

	public interface IAdministratorService
	{
		Task ValidateAdministrativePrivileges(Guid userId, Guid groupId);
	}
}
