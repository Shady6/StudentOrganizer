using System;
using Newtonsoft.Json;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.Commands.Schedules
{
	public class AddSchedule
	{
		[JsonIgnore]
		public string TeamName { get; set; }

		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }

		public ScheduleDto Schedule { get; set; }
	}
}