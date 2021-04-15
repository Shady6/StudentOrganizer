using System.Collections.Generic;

namespace StudentOrganizer.Infrastructure.Dto
{
	public class StudentDto : DisplayUserDto
	{
		public List<string> Teams { get; set; }
	}
}