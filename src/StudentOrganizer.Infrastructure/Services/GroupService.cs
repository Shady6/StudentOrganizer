using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Group;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;

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
			var foundGroup = await _groupRepository.GetAsync(command.Name);

			if (foundGroup != null)
				throw new Exception($"Group with name ${command.Name} already exists.");

			var group = new Group(command.Id, command.Name);
			var author = await _userRepository.GetAsync(command.UserId);
			group.AddAdministrator(author);
			group.AddStudent(author);

			await _groupRepository.AddAsync(group);
		}

		public List<GroupDto> GetMyGroup(GetMyGroup command)
		{
			throw new NotImplementedException();
		}

		public List<PublicGroupDto> GetAllGroups()
		{
			var groups = _groupRepository.GetAll();
			var groupsDto = _mapper.ProjectTo<PublicGroupDto>(groups).ToList();
			return groupsDto;
		}

		public List<SmallGroupDto> GetMyGroups(GetMyGroups command)
		{
			var groups = _groupRepository.GetAll()
				.Where(g => g.Students.Any(s => s.Id == command.UserId));

			var groupsDto = groups.Select(g => new SmallGroupDto
			{
				Name = g.Name,
				StudentsCount = g.Students.Count(),
				AssignmentsInProgress = g.Teams
				.Where(t => t.Students.Any(s => s.Id == command.UserId))
				.Sum(t => t.Assignmets.Count())
				+
				g.Assignmets.Count()
			}).ToList();

			return groupsDto;
		}
	}
}