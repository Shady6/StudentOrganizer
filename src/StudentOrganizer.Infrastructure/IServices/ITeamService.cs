using System.Threading.Tasks;
using StudentOrganizer.Infrastructure.Commands.Teams;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface ITeamService
	{
		Task AddTeams(AddTeams command);
		Task DeleteTeam(DeleteTeam command);
		Task UpdateTeamName(UpdateTeamName command);
	}
}
