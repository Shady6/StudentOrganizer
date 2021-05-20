using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
	public class DeleteAssignmentFromTeam : DeleteAssignment, ITeamAssignment
	{
		[JsonIgnore]
		public string TeamName { get; set; }
	}
}