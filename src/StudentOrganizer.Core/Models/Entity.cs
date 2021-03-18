using System;

namespace StudentOrganizer.Core.Models
{
	public class Entity
	{
		public Guid Id { get; protected set; }
		public DateTime CreatedAt { get; }

		protected Entity()
		{
			Id = new Guid();
			CreatedAt = DateTime.UtcNow;
		}
	}


}