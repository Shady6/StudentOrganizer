using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
	public class GetSuggestedUsers
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }

		public string SearchLetters { get; set; }
	}
}