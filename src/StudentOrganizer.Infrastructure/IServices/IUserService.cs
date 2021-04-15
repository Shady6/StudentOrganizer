using StudentOrganizer.Infrastructure.Dto;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IUserService : IService
	{		
		Task LoginAsync(Guid id, string email, string password);		
		Task RegisterAsync(Guid id, string email, string username, string password, string firstName, string lastName, RoleDto role);
	}
}