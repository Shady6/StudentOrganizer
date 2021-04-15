using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentOrganizer.Infrastructure.AutoMapper;
using StudentOrganizer.Infrastructure.Contexts;
using StudentOrganizer.Infrastructure.Settings;

namespace StudentOrganizer.Api.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddSingleton(AutoMapperConfiguration.Initialize());

			services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.FullName.Contains("Infrastructure") || x.FullName.Contains("Api"))
				.ToArray());
			services.AddMemoryCache();
		}

		public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<EfCoreDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("PostgreConnection"),
			b => b.MigrationsAssembly(typeof(EfCoreDbContext).Assembly.FullName)));

			var jwtSettings = new JwtSettings();
			configuration.GetSection("jwt").Bind(jwtSettings);
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
							ValidIssuer = jwtSettings.Issuer,
							ValidateAudience = false
						};
					});
		}

		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentOrganizer.Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer",
							},
							Scheme = "Bearer",
							Name = "Bearer",
							In = ParameterLocation.Header,
						}, new List<string>()
					},
			});
			});
		}
	}
}