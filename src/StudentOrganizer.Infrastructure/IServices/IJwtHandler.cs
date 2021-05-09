using System;
using StudentOrganizer.Core.Enums;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IJwtHandler
	{
		JwtDto CreateToken(Guid userId, Role role);
	}
}