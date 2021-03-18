using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class User : Entity
	{
		private ISet<Group> _groups = new HashSet<Group>(); 
		public string Email { get; protected set; }
		public string Password { get; protected set; }
		public string Salt { get; protected set; }
		public string FirstName { get; protected set; }
		public string LastName { get; protected set; }
		public IEnumerable<Group> Groups => _groups;

        public User(string email, string password, string salt, string firstName, string lastName)
        {
			SetMail(email);
			SetPassword(password, salt);
			SetFirstName(firstName);
			SetLastName(lastName);
        }

		public void SetMail(string email)
		{
            if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception("Email can not be empty.");
			}
			Email = email;
		}

		public void SetPassword(string password, string salt)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
				throw new Exception("Email can not be empty.");
			}
            if (string.IsNullOrWhiteSpace(salt))
            {
				throw new Exception("Salt can not be empty.");
            }
			Password = password;
			Salt = salt;
		}

		public void SetFirstName(string firstName)
		{
			if (string.IsNullOrWhiteSpace(firstName))
			{
				throw new Exception("First name can not be empty.");
			}
			FirstName = firstName;
		}

		public void SetLastName(string lastName)
		{
			if (string.IsNullOrWhiteSpace(lastName))
			{
				throw new Exception("Last name can not be empty.");
			}
			LastName = lastName;
		}

		
	}
}