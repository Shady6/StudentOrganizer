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

		public TeamService(IGroupRepository groupRepository, IAdministratorService administratorService)
		{
			_groupRepository = groupRepository;
			_administratorService = administratorService;
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