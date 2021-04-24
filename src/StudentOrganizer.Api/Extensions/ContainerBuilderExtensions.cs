using System.Reflection;
using Autofac;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Contexts;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Mongo.Repositories;
using StudentOrganizer.Infrastructure.Services;

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

		public static void AddRepositories(this ContainerBuilder builder, bool isMongo = true)
		{
			if (!isMongo)
				builder.RegisterType<EfCoreDbContext>().InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(MongoUserRepository)))
				.Where(t => t.Name.StartsWith(isMongo ? "Mongo" : "EfCore") && t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces();
		}
	}
}