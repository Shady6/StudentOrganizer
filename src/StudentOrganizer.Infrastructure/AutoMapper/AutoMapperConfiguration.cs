using AutoMapper;
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
				cfg.CreateMap<ScheduledCourseDto, ScheduledCourse>().ReverseMap();
				cfg.CreateMap<AddCourseDto, Course>();
				cfg.CreateMap<UpdateCourseDto, Course>();
				cfg.CreateMap<User, DisplayUserDto>();
				cfg.CreateMap<User, StudentDto>();
				cfg.CreateMap<Team, TeamDto>();
				cfg.CreateMap<Schedule, ScheduleDto>().ReverseMap();
				cfg.CreateMap<Group, GroupDto>();
				cfg.CreateMap<Group, PublicGroupDto>();
			})
			.CreateMapper();
	}
}