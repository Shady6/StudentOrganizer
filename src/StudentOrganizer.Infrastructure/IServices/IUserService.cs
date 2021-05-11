using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.Users.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IUserService : IService
	{
		Task<List<SuggestedUserDto>> GetSuggestedUsers(GetSuggestedUsers command);
		Task LeaveGroup(LeaveGroup command);
		Task LoginAsync(Guid id, string email, string password);		
        Task RegisterAsync(Guid guid, string email, string password, string firstName, string lastName, RoleDto role);
        Task LeaveTeam(LeaveTeam command);
        Task EditData(EditUser command);
    }
}