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
		List<PublicGroupDto> GetAllGroups(GetPublicGroups command);

		Task<GroupDto> GetMyGroup(GetMyGroup command);

		List<SmallGroupDto> GetMyGroups(GetMyGroups command);
		List<GroupDto> GetMyGroupsFull(GetMyGroups command);
	}
}