using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class User : Entity
	{
		private ISet<Group> _groups = new HashSet<Group>();
		private ISet<Group> _administratedGroups = new HashSet<Group>();
		public string Email { get; protected set; }
		public string PasswordHash { get; protected set; }
		public string Salt { get; protected set; }
		public string FirstName { get; protected set; }
		public string LastName { get; protected set; }
		public Role Role { get; protected set; }

		public IEnumerable<Group> Groups
		{
			get => _groups;
			protected set { _groups = new HashSet<Group>(value); }
		}

		public IEnumerable<Group> AdministratedGroups
		{
			get => _administratedGroups;
			protected set { _administratedGroups = new HashSet<Group>(value); }
		}

		public User(string email, string password, string salt, string firstName, string lastName)
		{
			SetMail(email);
			SetPassword(password, salt);
			SetFirstName(firstName);
			SetLastName(lastName);
		}

		public User(Guid userId)
		{
			Id = userId;
		}

		public User()
		{
		}

		public void SetMail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception("Email cannot be empty.");
			}
			Email = email;
		}

		public void SetPassword(string passwordHash, string salt)
		{
			if (string.IsNullOrWhiteSpace(passwordHash))
			{
				throw new Exception("Password hash cannot be empty.");
			}
			if (string.IsNullOrWhiteSpace(salt))
			{
				throw new Exception("Salt cannot be empty.");
			}
			PasswordHash = passwordHash;
			Salt = salt;
		}

		public void SetFirstName(string firstName)
		{
			if (string.IsNullOrWhiteSpace(firstName))
			{
				throw new Exception("First name cannot be empty.");
			}
			FirstName = firstName;
		}

		public void SetLastName(string lastName)
		{
			if (string.IsNullOrWhiteSpace(lastName))
			{
				throw new Exception("Last name cannot be empty.");
			}
			LastName = lastName;
		}

		public void AddGroup(Group group)
		{
			_groups.Add(group);
		}
	}
}