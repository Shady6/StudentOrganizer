using System;

namespace StudentOrganizer.Core.Models
{
	public class Entity
	{
		public Guid Id { get; protected set; }
		public DateTime CreatedAt { get; protected set; }

		protected Entity()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
		}				

		public T ConvertToIdOnly<T>() where T: Entity, new()
		{
			return new T
			{
				Id = Id
			};
		}
	}
}