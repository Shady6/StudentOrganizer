using AutoMapper;
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

            })
            .CreateMapper();
    }
}
