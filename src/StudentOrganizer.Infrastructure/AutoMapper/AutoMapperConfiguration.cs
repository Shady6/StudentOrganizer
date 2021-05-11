using System;
using AutoMapper;
using StudentOrganizer.Core.Enums;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.AutoMapper
{
	public static class AutoMapperConfiguration
	{
		public static IMapper Initialize()
			=> new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Address, AddressDto>().ReverseMap();
				cfg.CreateMap<Assignment, AssignmentDto>();
				cfg.CreateMap<Location, LocationDto>().ReverseMap();
				cfg.CreateMap<Course, CourseDto>().ReverseMap();
				cfg.CreateMap<Identifier, Course>();
				cfg.CreateMap<ScheduledCourseDto, ScheduledCourse>().ReverseMap();
				cfg.CreateMap<AddCourseDto, Course>();
				cfg.CreateMap<UpdateCourseDto, Course>();
				cfg.CreateMap<User, DisplayUserDto>();
				cfg.CreateMap<User, StudentDto>();				
				cfg.CreateMap<Team, TeamDto>();
				cfg.CreateMap<Schedule, ScheduleDto>().ReverseMap();
				cfg.CreateMap<Group, GroupDto>();
				cfg.CreateMap<Group, PublicGroupDto>();

				cfg.CreateMap<GroupToLeave, EntityToLeave>()
				.ConvertUsing((gtl, _) =>
				{
					return gtl switch
					{
						GroupToLeave.Group => EntityToLeave.Group,
						GroupToLeave.Moderation => EntityToLeave.Moderation,
						GroupToLeave.Administration => EntityToLeave.Administration,
						_ => throw new ArgumentException("Mapping error - case not handled in switch", nameof(gtl))
					};
				});
				cfg.CreateMap<RemoveFromGroupRoleDto, Role>()
				.ConvertUsing((rfgr, _) =>
				{
					return rfgr switch
					{
						RemoveFromGroupRoleDto.Student => Role.Student,
						RemoveFromGroupRoleDto.Moderator => Role.Moderator,						
						_ => throw new ArgumentException("Mapping error - case not handled in switch", nameof(rfgr))
					};
				});
			})
			.CreateMapper();
	}
}