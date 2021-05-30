using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Enums;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Users.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IGroupRepository _groupRepository;
		private readonly IEncrypter _encrypter;
		private readonly IJwtHandler _jwtHandler;
		private readonly IMemoryCache _memoryCache;
		private readonly IAdministratorService _administratorService;
		private readonly IMapper _mapper;

		public UserService(
			IUserRepository userRepository,
			IEncrypter encrypter,
			IJwtHandler jwtHandler,
			IMemoryCache memoryCache,
			IAdministratorService administratorService,
			IMapper mapper, IGroupRepository groupRepository)
		{
			_userRepository = userRepository;
			_encrypter = encrypter;
			_jwtHandler = jwtHandler;
			_memoryCache = memoryCache;
			_administratorService = administratorService;
			_mapper = mapper;
			_groupRepository = groupRepository;
		}

		public async Task SetAvatar(SetAvatar command)
		{
			var user = await _userRepository.GetAsync(command.UserId);
			if (!string.IsNullOrWhiteSpace(user.ImageHttpPath))
				throw new AppException("User already has an avatar. If you want to update it please use dedicated updating functionality.", AppErrorCode.ALREADY_EXISTS);
			var imageName =
				user.Email + "_" +
				DateTime.Now.ToString("dd_mm_yy_HH_MM_ss") +
				Path.GetExtension(command.ImageFile.FileName);

			await SaveImage(command.ImageFile, command.ImagesFolderPath, imageName);
			user.SetImage(command.ImageBaseHttpPath + "/" + imageName);
			await _userRepository.SaveChangesAsync();
		}

		private async Task SaveImage(IFormFile imageFile, string imagesFolderPath, string imageName)
		{
			var path = Path.Combine(imagesFolderPath, imageName);
			using (var fs = new FileStream(path, FileMode.Create))
			{
				await imageFile.CopyToAsync(fs);
			}
		}

		public async Task LeaveGroup(LeaveGroup command)
		{
			var user = await _userRepository.GetWithAllGroupsAsync(command.UserId);

			user.Leave(command.GroupId, _mapper.Map<EntityToLeave>(command.GroupToLeave));

			await _userRepository.SaveChangesAsync();
		}

		public async Task LeaveTeam(LeaveTeam command)
		{
			var group = await _groupRepository.GetWithTeamsAsync(command.GroupId);
			var user = await _userRepository.GetWithTeamsAsync(command.UserId);

			var teamId = group.Teams.FirstOrDefault(t => t.Name == command.TeamName)?.Id ?? Guid.NewGuid();
			user.Leave(teamId, EntityToLeave.Team);

			await _userRepository.SaveChangesAsync();
		}

		public async Task<List<DisplayUserDto>> GetSuggestedUsers(GetSuggestedUsers command)
		{
			if (string.IsNullOrWhiteSpace(command.SearchLetters) || command.SearchLetters.Length < 3)
				throw new AppException("You need to specify at least three letters to search suggested users", AppErrorCode.BAD_INPUT);
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);

			var foundUsers = _userRepository.GetSuggestedAsync(command.SearchLetters, command.GroupId).Take(10);
			return _mapper.ProjectTo<DisplayUserDto>(foundUsers).ToList();
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

		public async Task<UserLoginData> LoginAsync(LoginUserCommand command)
		{
			var user = await _userRepository.GetWithGroupTeamsAndStudents(command.Email);

			if (user == null)
				throw new AppException("Wrong email or password.", AppErrorCode.VALIDATION_ERROR);

			string generatedHash = _encrypter.GetHash(command.Password, user.Salt);
			if (user.PasswordHash != generatedHash)
				throw new AppException("Wrong email or password.", AppErrorCode.VALIDATION_ERROR);

			JwtDto token = _jwtHandler.CreateToken(user.Id, user.Role);
			return new UserLoginData
			{
				Token = token,
				User = new LoginUser
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					Groups = user.Groups.Select(g => new LoginGroupDto
					{
						Name = g.Name,
						Teams = g.Teams.Select(t => t.Name).ToList(),
						UserTeams = g.Teams.Where(t =>
						t.Students.Any(s => s.Id == user.Id))
						.Select(t => t.Name).ToList()
					}).ToList()
				}
			};	
		}
	}
}