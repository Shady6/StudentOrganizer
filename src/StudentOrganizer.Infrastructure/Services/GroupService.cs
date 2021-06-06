using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Enums;
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
		private readonly IAdministratorService _administratorService;

		public GroupService(IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository, IAdministratorService administratorService)
		{
			_groupRepository = groupRepository;
			_mapper = mapper;
			_userRepository = userRepository;
			_administratorService = administratorService;
		}

		public async Task EditGroupName(EditGroupName command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetAsync(command.GroupId);

			var isNotUniqueName = _groupRepository.GetAll().Select(n => n.Name).Contains(command.NewName);
			if (isNotUniqueName)
				throw new AppException($"A group with name {command.NewName} already exists.", AppErrorCode.ALREADY_EXISTS);
			group.SetName(command.NewName);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task RemoveUsersFromGroup(RemoveUsersFromGroup command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithAllUsers(command.GroupId);

			group.RemoveUsersFromGroup(command.Emails, command.UserId, _mapper.Map<Role>(command.Role));

			await _groupRepository.SaveChangesAsync();
		}

		public async Task AddUsersToGroup(AddUsersToGroup command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithStudents(command.GroupId);
			var users = await _userRepository.GetUsersByEmailsAsync(command.Emails);

			group.AddStudents(users);
			await _groupRepository.SaveChangesAsync();

			var usersNotExistingInDb = command.Emails.Where(e => !users.Select(u => u.Email).Contains(e));

			if (usersNotExistingInDb.ToList().Count != 0)
				throw new AppException($"Users with those emails don't exist {string.Join(", ", usersNotExistingInDb)}." +
					$"Other users were added successfully.", AppErrorCode.DOESNT_EXIST);
		}

		public async Task PromoteUserToModerator(PromoteUserToModerator command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithAllUsers(command.GroupId);

			group.PromoteToMod(command.UserEmailToPromote);
			await _groupRepository.SaveChangesAsync();
		}

		public async Task CreateAsync(CreateGroup command)
		{
			var foundGroup = await _groupRepository.GetAsync(command.Name);

			if (foundGroup != null)
				throw new AppException($"Group with name ${command.Name} already exists.", AppErrorCode.ALREADY_EXISTS);

			var group = new Group(command.Id, command.Name);
			var author = await _userRepository.GetAsync(command.UserId);
			group.AddAdministrator(author);
			group.AddStudent(author);

			await _groupRepository.AddAsync(group);
			await _groupRepository.SaveChangesAsync();
		}

		public async Task DeleteGroup(DeleteGroup command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			_groupRepository.Delete(command.GroupId);
			await _groupRepository.SaveChangesAsync();
		}

		public async Task<GroupDto> GetMyGroup(GetMyGroup command)
		{
			var group = await _groupRepository.GetWholeGroupAsync(command.GroupId);
			if (group == null)
				throw new AppException($"Group doesn't exist", AppErrorCode.DOESNT_EXIST);
			else if (!group.Students.Any(s => s.Id == command.UserId) &&
				!group.Administrators.Any(a => a.Id == command.UserId))
				throw new AppException("You don't belong to this group", AppErrorCode.CANT_DO_THAT);

			return CreateFullGroup().Compile().Invoke(group);
		}

		public List<GroupDto> GetMyGroupsFull(GetMyGroups command)
		{
			IQueryable<Group> groups = _groupRepository.GetWholeGroupsAsync(command.UserId);

			return groups.Select(CreateFullGroup().Compile()).ToList();
		}

		private Expression<Func<Group, GroupDto>> CreateFullGroup()
			=> (g) => new GroupDto
			{
				Id = g.Id,
				Administrators = _mapper.Map<List<DisplayUserDto>>(g.Administrators),
				Moderators = _mapper.Map<List<DisplayUserDto>>(g.Moderators),
				Courses = _mapper.Map<List<CourseDto>>(g.Courses),
				Name = g.Name,
				Assignments = g.Assignmets.Select(a => new AssignmentDto
				{
					Semester = a.Semester,
					Deadline = a.Deadline,
					Description = a.Description,
					Id = a.Id,
					Name = a.Name,
					Course = _mapper.Map<CourseDto>(a.Course)
				}).ToList(),
				Schedules = _mapper.Map<List<ScheduleDto>>(g.Schedules),
				Teams = g.Teams.Select(t =>
				new TeamDto
				{
					Assignments = t.Assignmets.Select(a => new AssignmentDto
					{
						Semester = a.Semester,
						Deadline = a.Deadline,
						Description = a.Description,
						Id = a.Id,
						Name = a.Name,
						Course = _mapper.Map<CourseDto>(a.Course)
					}).ToList(),
					Name = t.Name,
					Schedules = t.Schedules.Select(s =>
					new ScheduleDto
					{
						Semester = s.Semester,
						ScheduledCourses = _mapper.Map<List<ScheduledCourseDto>>(s.ScheduledCourses)
					}).ToList()
				}).ToList(),
				Students = g.Students.Select(s => new StudentDto
				{
					FirstName = s.FirstName,
					LastName = s.LastName,
					Email = s.Email,
					Teams = g.Teams.Where(t => t.Students.Any(st => st.Id == s.Id))
					.Select(t => t.Name).ToList()
				}).ToList()
			};

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