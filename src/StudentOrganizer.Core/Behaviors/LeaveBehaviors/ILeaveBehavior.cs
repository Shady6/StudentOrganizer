using System;

namespace StudentOrganizer.Core.Behaviors.LeaveBehaviors
{
	public interface ILeaveBehavior
	{
		public bool Leave(Guid leavedEntityId);
	}
}