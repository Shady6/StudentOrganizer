using StudentOrganizer.Infrastructure.Dto;
using System;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IJwtHandler
	{
		JwtDto CreateToken(Guid userId, RoleDto role);
	}
}