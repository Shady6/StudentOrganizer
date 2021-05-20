using System;
using System.Collections.Generic;
using StudentOrganizer.Core.Behaviors.LeaveBehaviors;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Enums;

namespace StudentOrganizer.Core.Models
{
	public class User : Entity
	{
		private ISet<Group> _groups = new HashSet<Group>();
		private ISet<Group> _administratedGroups = new HashSet<Group>();
		private ISet<Group> _moderatedGroups = new HashSet<Group>();
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

		public IEnumerable<Group> ModeratedGroups
		{
			get => _moderatedGroups;
			protected set { _moderatedGroups = new HashSet<Group>(value); }
		}

		public ICollection<Team> Teams { get; protected set; }

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

		public void UpdateData(string email, string password, string salt, string firstName, string lastName)
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
				throw new AppException("Email cannot be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			Email = email;
		}

		public void SetPassword(string passwordHash, string salt)
		{
			if (string.IsNullOrWhiteSpace(passwordHash))
			{
				throw new AppException("Password hash cannot be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			if (string.IsNullOrWhiteSpace(salt))
			{
				throw new AppException("Salt cannot be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			PasswordHash = passwordHash;
			Salt = salt;
		}

		public void SetFirstName(string firstName)
		{
			if (string.IsNullOrWhiteSpace(firstName))
			{
				throw new AppException("First name cannot be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			FirstName = firstName;
		}

		public void SetLastName(string lastName)
		{
			if (string.IsNullOrWhiteSpace(lastName))
			{
				throw new AppException("Last name cannot be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			LastName = lastName;
		}

		public void Leave(Guid leavedEntityId, EntityToLeave entityToLeave)
		{
			ILeaveBehavior leaveBehavior;

			switch (entityToLeave)
			{
				case EntityToLeave.Group:
					leaveBehavior = new LeaveGroupBehavior(_groups, _moderatedGroups, _administratedGroups);
					break;

				case EntityToLeave.Moderation:
					leaveBehavior = new LeaveModeratorBehavior(_moderatedGroups);
					break;

				case EntityToLeave.Administration:
					leaveBehavior = new LeaveAdministrationBehavior(_administratedGroups);
					break;

				case EntityToLeave.Team:
					leaveBehavior = new LeaveTeamBehavior(Teams);
					break;

				default:
					throw new ArgumentException("Specified EntityToLeave is not handled.", nameof(entityToLeave));
			}
			leaveBehavior.Leave(leavedEntityId);
		}
	}
}