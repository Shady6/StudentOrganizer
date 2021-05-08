using System;
using System.Linq;
using System.Threading.Tasks;
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
		private readonly IUserRepository _userRepository;

		public TeamService(IGroupRepository groupRepository, IAdministratorService administratorService, IUserRepository userRepository)
		{
			_groupRepository = groupRepository;
			_administratorService = administratorService;
			_userRepository = userRepository;
		}

		public async Task AddUsersToTeam(AddUsersToTeam command)
		{
			await _administratorService.ValidateAdministrativePrivileges(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithTeamsAndStudentsAsync(command.GroupId);
			var users = await _userRepository.GetUsersByEmails(command.Emails);
			var team = group.Teams.FirstOrDefault(t => t.Name == command.TeamName);

			if (team == null)
				throw new Exception($"Team with this name doesn't exist {command.TeamName}");

			team.AddStudents(users);
			await _groupRepository.SaveChangesAsync();

			var usersNotExistingInDb = command.Emails.Where(e => !users.Select(u => u.Email).Contains(e));

			if (usersNotExistingInDb.ToList().Count != 0)
				throw new Exception($"Users with those emails don't exist {string.Join(", ", usersNotExistingInDb)}");
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