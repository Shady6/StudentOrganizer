using System;
using System.Text.Json.Serialization;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.Commands.Courses
{
	public class UpdateCourse
	{
		[JsonIgnore]
		public Guid UserId { get; set; }

		[JsonIgnore]
		public Guid GroupId { get; set; }		

		public UpdateCourseDto Course { get; set; }
	}
}