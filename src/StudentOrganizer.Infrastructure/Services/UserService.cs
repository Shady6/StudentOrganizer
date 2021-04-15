using Microsoft.Extensions.Caching.Memory;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IEncrypter _encrypter;
		private readonly IJwtHandler _jwtHandler;
		private readonly IMemoryCache _memoryCache;

		public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler, IMemoryCache memoryCache)
		{
			_userRepository = userRepository;
			_encrypter = encrypter;
			_jwtHandler = jwtHandler;
			_memoryCache = memoryCache;
		}

		public async Task RegisterAsync(Guid id, string email, string username, string password,
			string firstName, string lastName, RoleDto role)
		{			
			if (await _userRepository.GetAsync(email) != null)
				throw new Exception($"User with email {email} already exists.");

			string salt = _encrypter.GetSalt(password);
			string passwordHash = _encrypter.GetHash(password, salt);

			var user = new User(email, passwordHash, salt, firstName, lastName);
			await _userRepository.AddAsync(user);			
		}

		public async Task LoginAsync(Guid id, string email, string password)
		{
			var user = await _userRepository.GetAsync(email);

			if (user == null)
				throw new Exception("Wrong email or password.");

			string generatedHash = _encrypter.GetHash(password, user.Salt);
			if (user.PasswordHash != generatedHash)
				throw new Exception("Wrong email or password.");

			JwtDto token = _jwtHandler.CreateToken(user.Id, user.Role);
			_memoryCache.Set(id, token);
		}
	}
}