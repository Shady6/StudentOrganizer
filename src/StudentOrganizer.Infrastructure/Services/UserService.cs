using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IEncrypter _encrypter;

		public UserService(IUserRepository userRepository, IEncrypter encrypter)
		{
			_userRepository = userRepository;
			_encrypter = encrypter;
		}

		public async Task RegisterAsync(Guid id, string email, string username, string password,
			string firstName, string lastName, RoleDto role)
		{
			if (await _userRepository.GetAsync(id) != null)
				throw new Exception($"User with id {id} already exists.");
			else if (await _userRepository.GetAsync(email) != null)
				throw new Exception($"User with email {email} already exists.");

			string salt = _encrypter.GetSalt(password);
			string passwordHash = _encrypter.GetHash(password, salt);

			var user = new Core.Models.User(email, passwordHash, salt, firstName, lastName);
			await _userRepository.AddAsync(user);
			await _userRepository.SaveChangesAsync();
		}
	}
}