using System;

namespace StudentOrganizer.Core.Models
{
	public abstract class Entity
	{
		public Guid Id { get; protected set; }
		public DateTime CreatedAt { get; protected set; }

		protected Entity()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
		}
	}


}