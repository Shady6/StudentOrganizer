using System;

namespace StudentOrganizer.Core.Models
{
	public class Team : Entity
	{
		public string Name { get; protected set; }
		public Schedule Schedule { get; protected set; }

		public Team(string name, Schedule schedule)
		{
			SetName(name);
			Schedule = schedule;
		}

		public Team()
		{
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new Exception("Name can not be empty.");
			}
			Name = name;
		}
	}
}