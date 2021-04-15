using Autofac;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.IServices;
using StudentOrganizer.Infrastructure.Repositories;
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
			builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
			builder.RegisterType<MongoUserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
			builder.RegisterType<AssignmentService>().As<IAssignmentService>().InstancePerLifetimeScope();
			builder.RegisterType<MongoAssignmentRepository>().As<IAssignmentRepository>().SingleInstance();
			builder.RegisterType<GroupService>().As<IGroupService>().InstancePerLifetimeScope();
			builder.RegisterType<MongoGroupRepository>().As<IGroupRepository>().SingleInstance();
		}
	}
}