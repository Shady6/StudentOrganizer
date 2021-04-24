using System.Threading.Tasks;
using StudentOrganizer.Infrastructure.Commands.Courses;

namespace StudentOrganizer.Infrastructure.IServices
{
	public interface ICourseService
    {
        Task AddCourses(AddCourses command);
		Task DeleteCourse(DeleteCourse command);
		Task UpdateCourse(UpdateCourse command);
	}
}
