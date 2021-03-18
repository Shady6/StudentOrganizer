using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class User : Entity
	{
		private ISet<Group> _groups = new HashSet<Group>(); 
		public string Email { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public IEnumerable<Group> Groups => _groups;
		public DateTime CreatedAt { get; set; }

        public User(string mail, string password, string salt, string firstName, string lastName)
        {
			CreatedAt = DateTime.UtcNow;
        }

		public void SetMail(string email)
		{
            if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception("Email can not be empty.");
			}
			Email = email;
		}

		public void SetPassword(string password)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
				throw new Exception("Email can not be empty.");
			}
			Password = password;
		}

		public void SetMail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception("Email can not be empty.");
			}
			Email = email;
		}

		public void SetMail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception("Email can not be empty.");
			}
			Email = email;
		}

		public void SetMail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception("Email can not be empty.");
			}
			Email = email;
		}
	}
}