using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
	public class UpdateAssignmentInTeam : UpdateAssignment, ITeamAssignment
	{
		[JsonIgnore]
		public string TeamName { get; set; }
	}
}