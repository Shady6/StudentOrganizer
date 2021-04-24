using System;
using System.Linq;
using System.Security.Claims;

namespace StudentOrganizer.Api.Extentions
{
	public static class ClaimsPrincipalExtensions
	{
		public static Guid GetUserId(this ClaimsPrincipal user)
		{
			return Guid.Parse(user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
		}
	}
}