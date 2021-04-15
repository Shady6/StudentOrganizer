﻿using System.Linq;
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
				cfg.CreateMap<Address, AddressDto>();				
				cfg.CreateMap<Assignment, AssignmentDto>();
				cfg.CreateMap<Location, LocationDto>();
				cfg.CreateMap<Course, CourseDto>();
				cfg.CreateMap<User, DisplayUserDto>();
				cfg.CreateMap<User, StudentDto>();
				cfg.CreateMap<Team, TeamDto>();
				cfg.CreateMap<Schedule, ScheduleDto>();
				cfg.CreateMap<Group, GroupDto>();
				cfg.CreateMap<Group, PublicGroupDto>();				
				
				
			})
			.CreateMapper();
	}
}