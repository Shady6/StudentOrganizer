using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Schedules
{
	public class DeleteSchedule
	{
		[JsonIgnore]
		public string TeamName { get; set; }

		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }

		[JsonIgnore]
		public int Semester { get; set; }
	}
}