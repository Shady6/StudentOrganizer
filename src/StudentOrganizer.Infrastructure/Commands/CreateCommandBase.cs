using MediatR;
using System;
using Newtonsoft.Json;

namespace StudentOrganizer.Infrastructure.Commands
{
	public abstract class CreateCommandBase : IRequest
	{
		[JsonIgnore]
		public Guid Id { get; private set; } = Guid.NewGuid();
	}
}