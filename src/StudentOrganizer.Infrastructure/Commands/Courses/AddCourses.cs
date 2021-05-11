using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.Commands.Courses
{
	public class AddCourses
	{
		[JsonIgnore]
		public Guid UserId { get; set; }
		
		[JsonIgnore]
		public Guid GroupId { get; set; }

		public List<AddCourseDto> Courses { get; set; }
	}
}