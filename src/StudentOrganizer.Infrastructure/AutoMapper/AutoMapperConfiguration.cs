using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Assignment, AssignmentDto>();
            })
            .CreateMapper();
    }
}
