using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Users.Commands;

namespace StudentOrganizer.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IEncrypter _encrypter;
		private readonly IJwtHandler _jwtHandler;
		private readonly IMemoryCache _memoryCache;
		private readonly IAdministratorService _administratorService;
		private readonly IMapper __mapper;

		public UserService(
			IUserRepository userRepository,
			IEncrypter encrypter,
			IJwtHandler jwtHandler,
			IMemoryCache memoryCache,
			IAdministratorService administratorService,
			IMapper mapper)
		{
			_userRepository = userRepository;
			_encrypter = encrypter;
			_jwtHandler = jwtHandler;
			_memoryCache = memoryCache;
			_administratorService = administratorService;
			__mapper = mapper;
		}

		public async Task<List<SuggestedUserDto>> GetSuggestedUsers(GetSuggestedUsers command)
		{
			if (string.IsNullOrWhiteSpace(command.SearchLetters) || command.SearchLetters.Length < 3)
				throw new AppException("You need to specify at least three letters to search suggested users", AppErrorCode.BAD_INPUT);
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);

			var foundUsers = _userRepository.GetSuggestedAsync(command.SearchLetters, command.GroupId).Take(10);
			return __mapper.ProjectTo<SuggestedUserDto>(foundUsers).ToList();
		}

		public async Task RegisterAsync(Guid id, string email, string username, string password,
			string firstName, string lastName, RoleDto role)
		{
			if (await _userRepository.GetAsync(email) != null)
				throw new AppException($"User with email {email} already exists.", AppErrorCode.ALREADY_EXISTS);

			string salt = _encrypter.GetSalt(password);
			string passwordHash = _encrypter.GetHash(password, salt);

			var user = new User(email, passwordHash, salt, firstName, lastName);
			await _userRepository.AddAsync(user);
			await _userRepository.SaveChangesAsync();
		}

		public async Task LoginAsync(Guid id, string email, string password)
		{
			var user = await _userRepository.GetAsync(email);

			if (user == null)
				throw new AppException("Wrong email or password.", AppErrorCode.VALIDATION_ERROR);

			string generatedHash = _encrypter.GetHash(password, user.Salt);
			if (user.PasswordHash != generatedHash)
				throw new AppException("Wrong email or password.", AppErrorCode.VALIDATION_ERROR);

			JwtDto token = _jwtHandler.CreateToken(user.Id, user.Role);
			_memoryCache.Set(id, token);
		}
	}
}