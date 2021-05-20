using Autofac;
using StudentOrganizer.Infrastructure.Contexts;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Repositories.EfCore;
using StudentOrganizer.Infrastructure.Services;
using System.Reflection;

namespace StudentOrganizer.Api.Extensions
{
	public static class ContainerBuilderExtensions
	{
		public static void AddServices(this ContainerBuilder builder)
		{
			//to do modules
			builder.RegisterType<Encrypter>().As<IEncrypter>().SingleInstance();
			builder.RegisterType<JwtHandler>().As<IJwtHandler>().SingleInstance();

			builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(UserService)))
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}

		public static void AddRepositories(this ContainerBuilder builder)
		{
			builder.RegisterType<EfCoreDbContext>().InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(EfCoreUserRepository)))
				.Where(t => t.Name.StartsWith("EfCore") && t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces();
		}
	}
}