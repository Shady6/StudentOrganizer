using System.Collections.Generic;
using System.Buffers.Binary;

namespace StudentOrganizer.Core.Models
{
	public class Feed : Entity
	{
		public List<Post> Posts { get; set; }		
	}

	public class Post : Entity
	{
		public string Content { get; set; }
	}
}