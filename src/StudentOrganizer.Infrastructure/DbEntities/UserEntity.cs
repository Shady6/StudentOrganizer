using StudentOrganizer.Core.Models;
using System;
using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.DbEntities
{
	public class UserEntity : Entity
	{		
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string Salt { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Role Role { get; set; }
		public List<Guid> Groups { get; set; }
	}
}