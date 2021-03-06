using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using StudentOrganizer.Api.Exceptions;
using StudentOrganizer.Api.Extensions;
using System.IO;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddServices();
			services.AddContextInfrastructure(Configuration);					
			services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
				options.SerializerSettings.Formatting = Formatting.Indented;
			});
			services.AddSwagger();
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.AddServices();
			builder.AddRepositories();
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

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "UserAvatars")),
				RequestPath = "/UserAvatars"
			});

			app.UseRouting();
			app.UseMiddleware<ExceptionHandler>();

			app.UseAuthentication();
			app.UseAuthorization();			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}