using System;
using System.Collections.Generic;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Behaviors.LeaveBehaviors
{
	public class LeaveTeamBehavior : ILeaveBehavior
	{
		private readonly ILeaveBehavior leaveBahvior;

		public LeaveTeamBehavior(ICollection<Team> Teams)
		{
			this.leaveBahvior = new LeaveEntityBehavior<Team>(Teams);
		}

		public bool Leave(Guid teamId)
		{
			if (!leaveBahvior.Leave(teamId))
				throw new AppException("You're not attending specified team", AppErrorCode.CANT_DO_THAT);
			return true;
		}
	}
}