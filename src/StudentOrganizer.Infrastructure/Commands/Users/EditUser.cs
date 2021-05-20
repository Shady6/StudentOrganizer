using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
    public class EditUser
    {
		[JsonIgnore]
		public Guid UserId { get; set; }
		public string NewEmail { get; set; }
		public string NewPassword { get; set; }
		public string NewFirstName { get; set; }
		public string NewLastName { get; set; }
	}
}