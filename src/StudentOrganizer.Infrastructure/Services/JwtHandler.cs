using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.Extentions;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentOrganizer.Infrastructure.Services
{
	public class JwtHandler : IJwtHandler
	{
		private JwtSettings _jwtSettings;

		public JwtHandler(IConfiguration configuration)
		{
			_jwtSettings = new JwtSettings();
			configuration.GetSection("jwt").Bind(_jwtSettings);
		}

		public JwtDto CreateToken(Guid userId, Role role)
		{
			var date = DateTime.UtcNow;
			var claims = new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
				new Claim(ClaimTypes.Role, role.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, date.ToTimestamp().ToString())
			};
			var expires = date.AddMinutes(15);
			var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
				SecurityAlgorithms.HmacSha256);
			var jwt = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				claims: claims,
				notBefore: date,
				expires: expires,
				signingCredentials: signingCredentials
			);
			var token = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new JwtDto
			{
				Value = token,
				Expires = expires.ToTimestamp()
			};
		}
	}
}