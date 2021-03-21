﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.User.Commands;
using System;
using System.Threading.Tasks;

namespace StudentOrganizer.Api.Controllers
{
	public class UserController : ControllerBase
	{
		protected readonly IMediator _mediator;
		private readonly IMemoryCache _memoryCache;

		public UserController(IMediator mediatr, IMemoryCache memoryCache)
		{
			_mediator = mediatr;
			_memoryCache = memoryCache;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
		{
			await _mediator.Send(command);
			return Created("/users", null);
		}

		[HttpPost("login")]
		public async Task<ActionResult<JwtDto>> Login([FromBody] LoginUserCommand command)
		{
			command.Id = Guid.NewGuid();
			await _mediator.Send(command);
			var jwt = _memoryCache.Get<JwtDto>(command.Id);
			return Ok(jwt);
		}
	}
}