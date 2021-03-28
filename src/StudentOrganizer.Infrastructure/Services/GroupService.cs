using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Group;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Services
{
	public class GroupService : IGroupService
	{
		private readonly IGroupRepository _groupRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public GroupService(IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository)
		{
			_groupRepository = groupRepository;
			_mapper = mapper;
			_userRepository = userRepository;
		}

		public async Task CreateAsync(CreateGroup command)
		{
			var group = new Group(command.Id, command.Name);
			var foundGroup = await _groupRepository.GetAsync(group.Name);

			if (foundGroup != null)
				throw new Exception($"Group with name ${group.Name} already exists.");

			var user = await _userRepository.GetAsync(command.AuthorId);

			group.AddAdministrator(user.ConvertToIdOnly<User>());
			user.AddGroup(group.ConvertToIdOnly<Group>());

			await _groupRepository.AddAsync(group);
			await _userRepository.UpdateAsync(user);
		}
	}
}