using StudentOrganizer.Infrastructure.Commands.Schedules;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IScheduleService
    {
        Task AddTeamSchedule(AddSchedule command);
		Task DeleteTeamSchedule(DeleteSchedule command);
		Task UpdateTeamSchedule(UpdateSchedule command);
	}
}
