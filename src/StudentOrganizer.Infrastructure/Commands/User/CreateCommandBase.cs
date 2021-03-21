using MediatR;
using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.User.Commands
{
	public abstract class CreateCommandBase : IRequest
	{		
		[JsonIgnore]
		public Guid Id { get; set; }
	}
}