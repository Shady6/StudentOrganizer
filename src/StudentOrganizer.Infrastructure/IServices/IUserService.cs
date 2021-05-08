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
		Task LoginAsync(Guid id, string email, string password);		
		Task RegisterAsync(Guid id, string email, string username, string password, string firstName, string lastName, RoleDto role);
	}
}