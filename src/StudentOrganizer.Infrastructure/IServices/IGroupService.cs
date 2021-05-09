using System.Collections.Generic;
using System.Threading.Tasks;
using StudentOrganizer.Infrastructure.Commands.Groups;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IGroupService
	{
		Task AddUsersToGroup(AddUsersToGroup command);

		Task CreateAsync(CreateGroup command);

		Task EditGroupName(EditGroupName command);

		List<PublicGroupDto> GetAllGroups(GetPublicGroups command);

		Task<GroupDto> GetMyGroup(GetMyGroup command);

		List<SmallGroupDto> GetMyGroups(GetMyGroups command);

		Task DeleteGroup(DeleteGroup command);

		List<GroupDto> GetMyGroupsFull(GetMyGroups command);

		Task PromoteUserToModerator(PromoteUserToModerator command);
	}
}