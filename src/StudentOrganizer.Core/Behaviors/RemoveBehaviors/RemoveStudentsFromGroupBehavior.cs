using System;
using System.Collections.Generic;
using System.Linq;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Behaviors.RemoveBehaviors
{
	public class RemoveStudentsFromGroupBehavior : IRemoveUsersBehavior
	{
		private readonly ICollection<User> students;
		private readonly ICollection<User> moderators;
		private readonly ICollection<User> administrators;

		public RemoveStudentsFromGroupBehavior(
			ICollection<User> students,
			ICollection<User> moderators,
			ICollection<User> administrators)
		{
			this.students = students;
			this.moderators = moderators;
			this.administrators = administrators;
		}

		public void Remove(List<string> emails, Guid removerId)
		{
			var usersNotExisting = new List<string>();
			var selfDeleteMsg = "";
			var adminDeleteMsg = "";

			foreach (var email in emails)
			{
				var foundUser = students.FirstOrDefault(s => s.Email == email);

				if (foundUser.Id == removerId)
					selfDeleteMsg = $"Can't remove yourself from the group. Please use dedicated funcionality for that. ";
				else if (administrators.Any(a => a.Id == foundUser.Id))
					adminDeleteMsg = $"You can't remove {foundUser.Email} from the group because he is an admin. ";
				else if (foundUser != null)
				{
					students.Remove(foundUser);
					var mod = moderators.FirstOrDefault(m => m.Id == foundUser.Id);
					moderators.Remove(mod);
				}
				else
					usersNotExisting.Add(foundUser.Email);
			}

			if (usersNotExisting.Count > 0)
				throw new AppException($"{adminDeleteMsg}{selfDeleteMsg}" +
					$"Students with those emails don't exist in the group {string.Join(", ", usersNotExisting)}." +
					$"Other students were removed successfully", AppErrorCode.DOESNT_EXIST);
		}
	}
}