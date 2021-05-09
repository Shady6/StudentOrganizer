using System;
using System.Collections.Generic;
using System.Linq;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Behaviors.RemoveBehaviors
{
	public class RemoveBehavior : IRemoveUsersBehavior
	{
		private readonly ICollection<User> users;
		private readonly string removeFrom;
		private readonly string removeWho;

		public RemoveBehavior(ICollection<User> users, string removeFrom, string removeWho)
		{
			this.users = users;
			this.removeFrom = removeFrom;
			this.removeWho = removeWho;
		}

		public void Remove(List<string> emails, Guid removerId)
		{
			var usersNotExisting = new List<string>();
			var selfDeleteMsg = "";

			foreach (var email in emails)
			{
				var foundUsers = users.FirstOrDefault(s => s.Email == email);

				if (foundUsers.Id == removerId)
					selfDeleteMsg = $"Can't remove yourself from the {removeFrom}. Please use dedicated funcionality for that. ";
				else if (foundUsers != null)
					users.Remove(foundUsers);
				else
					usersNotExisting.Add(foundUsers.Email);
			}

			if (usersNotExisting.Count > 0)
				throw new AppException($"{selfDeleteMsg}{removeWho} with those emails don't exist in the {removeFrom} {string.Join(", ", usersNotExisting)}." +
					$"Other {removeWho} were removed successfully", AppErrorCode.DOESNT_EXIST);
		}
	}
}