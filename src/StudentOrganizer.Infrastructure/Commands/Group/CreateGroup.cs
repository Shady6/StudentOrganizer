﻿using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Group
{
	public class CreateGroup : CreateCommandBase
	{
		public string Name { get; set; }

		[JsonIgnore]
		public Guid AuthorId { get; set; }
	}
}