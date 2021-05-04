using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Schedules;
using StudentOrganizer.Infrastructure.Factories;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Infrastructure.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly IGroupRepository _groupRepository;
		private readonly IAdministratorService _administratorService;
		private readonly IMapper _mapper;

		public ScheduleService(IGroupRepository groupRepository, IAdministratorService administratorService, IMapper mapper)
		{
			_groupRepository = groupRepository;
			_administratorService = administratorService;
			_mapper = mapper;
		}

		public async Task AddTeamSchedule(AddSchedule command)
		{
			await _administratorService.ValidateAdministrativePrivileges(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamScheduleAndCourses(command.GroupId, command.TeamName);

			if (group == null)
				throw new Exception($"Team with name {command.TeamName} doesn't exist.");

			var schedule = ScheduleFactory.Create(command.Schedule, group.Courses);
			var team = group.Teams.First(t => t.Name == command.TeamName);
			team.AddSchedule(schedule);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task DeleteTeamSchedule(DeleteSchedule command)
		{
			await _administratorService.ValidateAdministrativePrivileges(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamScheduleAndCourses(command.GroupId, command.TeamName);

			if (group == null)
				throw new Exception($"Team with name {command.TeamName} doesn't exist.");

			var team = group.Teams.First(t => t.Name == command.TeamName);
			team.DeleteSchedule(command.Semester);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task UpdateTeamSchedule(UpdateSchedule command)
		{
			await _administratorService.ValidateAdministrativePrivileges(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamScheduleAndCourses(command.GroupId, command.TeamName);

			if (group == null)
				throw new Exception($"Team with name {command.TeamName} doesn't exist.");

			var schedule = ScheduleFactory.Create(command.Schedule, group.Courses);
			var scheduleToUpdate = group.Teams.First(t => t.Name == command.TeamName)
				.Schedules.FirstOrDefault(s => s.Semester == command.Schedule.Semester);

			if (scheduleToUpdate == null)
				throw new Exception($"There is no schedule to update for semester {command.Schedule.Semester}");

			scheduleToUpdate.Update(schedule.Semester, schedule.ScheduledCourses);

			await _groupRepository.SaveChangesAsync();
		}
	}
}