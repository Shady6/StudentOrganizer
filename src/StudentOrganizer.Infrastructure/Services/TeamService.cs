using System;
using System.Linq;
using System.Threading.Tasks;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Teams;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Infrastructure.Services
{
	public class TeamService : ITeamService
	{
		private readonly IGroupRepository _groupRepository;
		private readonly IAdministratorService _administratorService;

		public TeamService(IGroupRepository groupRepository, IAdministratorService administratorService)
		{
			_groupRepository = groupRepository;
			_administratorService = administratorService;
		}

		public async Task AddUsersToTeam(AddUsersToTeam command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithGroupAndTeamStudents(command.GroupId);

			var groupStudents = group.Students.Where(s => command.Emails.Any(e => e == s.Email)).ToList();
			var team = group.Teams.FirstOrDefault(t => t.Name == command.TeamName);

			if (team == null)
				throw new AppException($"Team with this name doesn't exist {command.TeamName}", AppErrorCode.DOESNT_EXIST);

			team.AddStudents(groupStudents);
			await _groupRepository.SaveChangesAsync();

			var usersNotExistingInDb = command.Emails.Where(e => !groupStudents.Select(u => u.Email).Contains(e));

			if (usersNotExistingInDb.ToList().Count != 0)
				throw new AppException($"Users with those emails don't exist in your group {string.Join(", ", usersNotExistingInDb)}." +
					$"Other users were added successfully.", AppErrorCode.DOESNT_EXIST);
		}

		public async Task RemoveUsersFromTeam(RemoveUsersFromTeam command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamStudentsAsync(command.GroupId);
			var team = group.Teams.FirstOrDefault(t => t.Name == command.TeamName);

			if (team == null)
				throw new AppException($"Team {command.TeamName} doesn't exist", AppErrorCode.DOESNT_EXIST);

			team.RemoveStudents(command.Emails, command.UserId);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task AddTeams(AddTeams command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamsAsync(command.GroupId);

			var teams = command.TeamNames.Select(tn => new Team(tn));
			group.AddTeams(teams);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task DeleteTeam(DeleteTeam command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamsAsync(command.GroupId);

			group.DeleteTeam(command.TeamName);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task UpdateTeamName(UpdateTeamName command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamsAsync(command.GroupId);

			group.UpdateTeamName(command.TeamName, command.NewTeamName);

			await _groupRepository.SaveChangesAsync();
		}
	}
}