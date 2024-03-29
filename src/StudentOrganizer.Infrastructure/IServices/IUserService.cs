﻿using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.Users.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IUserService : IService
	{
		Task<List<DisplayUserDto>> GetSuggestedUsers(GetSuggestedUsers command);
		Task LeaveGroup(LeaveGroup command);		
		Task RegisterAsync(Guid id, string email, string username, string password, string firstName, string lastName, RoleDto role);
		Task LeaveTeam(LeaveTeam command);
		Task SetAvatar(SetAvatar command);
		Task<UserLoginData> LoginAsync(LoginUserCommand command);
	}
}