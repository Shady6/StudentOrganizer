using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Behaviors.RemoveBehaviors
{
	public interface IRemoveUsersBehavior
	{
		public void Remove(List<string> emails, Guid removerId);
	}
}