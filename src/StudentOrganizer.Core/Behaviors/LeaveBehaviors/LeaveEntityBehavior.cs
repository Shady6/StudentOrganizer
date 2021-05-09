using System;
using System.Collections.Generic;
using System.Linq;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Behaviors.LeaveBehaviors
{
	public class LeaveEntityBehavior<TEntity> : ILeaveBehavior
		where TEntity : Entity
	{
		private ICollection<TEntity> entities { get; set; }

		public LeaveEntityBehavior(ICollection<TEntity> entities)
		{
			this.entities = entities;
		}

		public bool Leave(Guid groupId)
		{
			var entity = entities.FirstOrDefault(e => e.Id == groupId);
			if (entity != null)
			{
				entities.Remove(entity);
				return true;
			}
			return false;
		}
	}
}