using System;
using System.Collections.Generic;
using System.Linq;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Behaviors.LeaveBehaviors
{
	public class LeaveAdministrationBehavior : ILeaveBehavior
	{
		private readonly ISet<Group> administratedGroups;

		public LeaveAdministrationBehavior(ISet<Group> administratedGroups)
		{
			this.administratedGroups = administratedGroups;
		}

		public bool Leave(Guid groupId)
		{
			var administratedGroup = administratedGroups.FirstOrDefault(g => g.Id == groupId);
			if (administratedGroup == null)
				throw new AppException("You're not administrator of the specified group", AppErrorCode.CANT_DO_THAT);
			else if (administratedGroup.Administrators.Count() == 1)
				throw new AppException("You're the last administrator of the group. Promote someone else to administrator before leaving administration.", AppErrorCode.CANT_DO_THAT);
			else
			{
				administratedGroups.Remove(administratedGroup);
				return true;
			}
		}
	}
}