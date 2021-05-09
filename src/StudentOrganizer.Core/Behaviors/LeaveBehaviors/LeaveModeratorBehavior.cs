using System;
using System.Collections.Generic;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Behaviors.LeaveBehaviors
{
	public class LeaveModeratorBehavior : ILeaveBehavior
	{
		private readonly ILeaveBehavior leaveBahvior;

		public LeaveModeratorBehavior(ISet<Group> moderatedGroups)
		{
			this.leaveBahvior = new LeaveEntityBehavior<Group>(moderatedGroups);
		}

		public bool Leave(Guid groupId)
		{
			if (!leaveBahvior.Leave(groupId))
				throw new AppException("You're not moderator of the specified group", AppErrorCode.CANT_DO_THAT);
			return true;
		}
	}
}