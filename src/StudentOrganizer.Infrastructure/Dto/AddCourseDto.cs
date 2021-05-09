namespace StudentOrganizer.Infrastructure.Dto
{
	public class AddCourseDto
	{
		public string Name { get; set; }
		public string Lecturer { get; set; }
		public LocationDto Location { get; set; }
		public int Semester { get; set; }
	}
}