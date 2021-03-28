using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Infrastructure.DbEntities;
using StudentOrganizer.Infrastructure.Dto;
using System.Linq;

namespace StudentOrganizer.Infrastructure.AutoMapper
{
	public static class AutoMapperConfiguration
	{
		public static IMapper Initialize()
			=> new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Assignment, AssignmentDto>();

				cfg.CreateMap<Group, GroupEntity>().
				ForMember(d => d.Administrators, m => m.MapFrom(o => o.Administrators.Select(a => a.Id).ToList()));
				cfg.CreateMap<User, UserEntity>().
				ForMember(d => d.Groups, m => m.MapFrom(o => o.Groups.Select(a => a.Id).ToList()));
			})
			.CreateMapper();
	}
}