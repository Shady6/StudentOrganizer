using System;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Group;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Infrastructure.Services
{
	public class GroupService : IGroupService
	{
		private readonly IGroupRepository _groupRepository;

		public GroupService(IGroupRepository groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task CreateAsync(CreateGroup command)
		{
			var foundGroup = await _groupRepository.GetAsync(command.Name);

			if (foundGroup != null)
				throw new Exception($"Group with name ${command.Name} already exists.");

			var group = new Group(command.Id, command.Name);
			group.AddAdministrator(new User(command.AuthorId));

			await _groupRepository.AddAsync(group);
		}
	}
}