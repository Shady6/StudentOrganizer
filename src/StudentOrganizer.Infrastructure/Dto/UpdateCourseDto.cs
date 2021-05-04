using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class UpdateCourseDto
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Lecturer { get; set; }
		public LocationDto Location { get; set; }
		public int Semester { get; set; }
	}

}