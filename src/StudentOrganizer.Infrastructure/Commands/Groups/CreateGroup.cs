using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
    public class CreateGroup : CreateCommandBase
	{
		public string Name { get; set; }

		[JsonIgnore]
		public Guid UserId { get; set; }
	}
}