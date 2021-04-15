using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StudentOrganizer.Infrastructure.AutoMapper;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Services;
using StudentOrganizer.Infrastructure.Settings;
using System.Text;
using MediatR;
using System;
using System.Linq;
using StudentOrganizer.Infrastructure.Repositories;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Mongo;
using System.Collections.Generic;

namespace StudentOrganizer.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; private set; }
		public ILifetimeScope AutofacContainer { get; private set; }

		public Startup(IWebHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
								.SetBasePath(env.ContentRootPath)
								.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
								.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
								.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//jwt
			var jwtSettings = new JwtSettings();
			Configuration.GetSection("jwt").Bind(jwtSettings);
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

			//automapper
			services.AddSingleton(AutoMapperConfiguration.Initialize());

			services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.FullName.Contains("Infrastructure") || x.FullName.Contains("Api"))
				.ToArray());
			services.AddMemoryCache();

			services.AddControllers();
			services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Formatting = Formatting.Indented);
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

		//Autofac
		public void ConfigureContainer(ContainerBuilder builder)
		{
			//to do modules
			builder.RegisterType<Encrypter>().As<IEncrypter>().SingleInstance();
			builder.RegisterType<JwtHandler>().As<IJwtHandler>().SingleInstance();
			builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();			
			builder.RegisterType<MongoUserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
			builder.RegisterType<AssignmentService>().As<IAssignmentService>().InstancePerLifetimeScope();
			builder.RegisterType<MongoAssignmentRepository>().As<IAssignmentRepository>().SingleInstance();
			builder.RegisterType<GroupService>().As<IGroupService>().InstancePerLifetimeScope();
			builder.RegisterType<MongoGroupRepository>().As<IGroupRepository>().SingleInstance();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentOrganizer.Api v1"));
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			MongoConfiguration.Initialize();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}