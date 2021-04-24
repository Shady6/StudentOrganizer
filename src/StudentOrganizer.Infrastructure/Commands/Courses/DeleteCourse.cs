using System;

namespace StudentOrganizer.Infrastructure.Commands.Courses
{
	public class DeleteCourse
	{		
		public Guid UserId { get; set; }		
		public Guid GroupId { get; set; }
		public Guid CourseId { get; set; }
	}
}