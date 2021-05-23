using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
	public class SetAvatar
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		public IFormFile ImageFile { get; set; }

		[JsonIgnore]
		public string ImagesFolderPath { get; set; }

		[JsonIgnore]
		public string ImageBaseHttpPath { get; set; }
	}
}