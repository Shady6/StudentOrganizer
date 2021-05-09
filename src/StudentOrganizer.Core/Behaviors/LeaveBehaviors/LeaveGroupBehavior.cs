using System;
using System.Collections.Generic;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Behaviors.LeaveBehaviors
{
	public class LeaveGroupBehavior : ILeaveBehavior
	{
		private readonly ILeaveBehavior leaveAttended;
		private readonly ILeaveBehavior leaveModerated;
		private readonly ILeaveBehavior leaveAdministrated;

		private readonly ISet<Group> administratedGroups;

		public LeaveGroupBehavior(ISet<Group> groups, ISet<Group> moderatedGroups, ISet<Group> administratedGroups)
		{
			leaveAttended = new LeaveEntityBehavior<Group>(groups);
			leaveModerated = new LeaveEntityBehavior<Group>(moderatedGroups);
			leaveAdministrated = new LeaveAdministrationBehavior(administratedGroups);

			this.administratedGroups = administratedGroups;
		}

		public bool Leave(Guid groupId)
		{
			bool leftAdministration;
			try
			{
				leftAdministration = leaveAdministrated.Leave(groupId);
			}
			catch (AppException)
			{
				if (administratedGroups.Count > 1)
					leftAdministration = false;
				else
					throw;
			}

			if (!(leaveAttended.Leave(groupId) ||
				leaveModerated.Leave(groupId) ||
				leftAdministration))
				throw new AppException("You don't belong to the specified group", AppErrorCode.CANT_DO_THAT);
			return true;
		}
	}
}