using MediatR;
using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands
{
	public abstract class CreateCommandBase : IRequest
	{
		[JsonIgnore]
		public Guid Id { get; private set; } = Guid.NewGuid();
	}
}