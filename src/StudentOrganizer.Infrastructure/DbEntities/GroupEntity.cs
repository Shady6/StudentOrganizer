using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.DbEntities
{
	public class GroupEntity : Entity
	{
		public string Name { get; set; }
		public List<Guid> Administrators { get; set; }
		public List<Guid> Students { get; set; }
		public List<Schedule> Schedules { get; set; }
		public List<Team> Teams { get; set; }
		public List<Course> Course { get; set; }
	}
}