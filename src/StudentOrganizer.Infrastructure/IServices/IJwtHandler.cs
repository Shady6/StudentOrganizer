using StudentOrganizer.Core.Models;
using StudentOrganizer.Infrastructure.Dto;
using System;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IJwtHandler
	{
		JwtDto CreateToken(Guid userId, Role role);
	}
}