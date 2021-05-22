using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
	public class AddAssignmentToTeam : AddAssignment, ITeamAssignment
	{
		[JsonIgnore]
		public string TeamName { get; set; }
	}
}