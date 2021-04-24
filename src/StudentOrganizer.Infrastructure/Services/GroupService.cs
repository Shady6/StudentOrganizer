using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Groups;
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
			await _groupRepository.SaveChangesAsync();
		}

		public async Task<GroupDto> GetMyGroup(GetMyGroup command)
		{
			var group = await _groupRepository.GetWholeGroupAsync(command.GroupId);
			if (group == null)
				throw new Exception($"Group doesn't exist");
			else if (!group.Students.Any(s => s.Id == command.UserId) ||
				!group.Administrators.Any(a => a.Id == command.UserId))
				throw new Exception("You don't belong to this group");

			return new GroupDto
			{
				Administrators = _mapper.Map<List<DisplayUserDto>>(group.Administrators),
				Courses = _mapper.Map<List<CourseDto>>(group.Courses),
				Name = group.Name,
				Assignments = _mapper.Map<List<AssignmentDto>>(group.Assignmets),
				Schedules = _mapper.Map<List<ScheduleDto>>(group.Schedules),
				Teams = _mapper.Map<List<TeamDto>>(group.Teams),
				Students = group.Students.Select(s => new StudentDto
				{
					FirstName = s.FirstName,
					LastName = s.LastName,
					Teams = group.Teams.Where(t => t.Students.Any(st => st.Id == s.Id))
					.Select(t => t.Name).ToList()
				}).ToList()
			};			
		}

		public List<PublicGroupDto> GetAllGroups(GetPublicGroups command)
		{
			var groups = _groupRepository.GetAll();
			var groupsDto = groups.Select(g => new PublicGroupDto
			{
				Id = g.Id,
				Name = g.Name,
				UserBelongsToGroup = g.Students.Any(s => s.Id == command.UserId) ||
				g.Administrators.Any(a => a.Id == command.UserId)
			});	

			return groupsDto.ToList();
		}

		public List<SmallGroupDto> GetMyGroups(GetMyGroups command)
		{
			var groups = _groupRepository.GetAll()
				.Where(g => g.Students.Any(s => s.Id == command.UserId));

			var groupsDto = groups.Select(g => new SmallGroupDto
			{
				Id = g.Id,
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