using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Courses;
using StudentOrganizer.Infrastructure.IServices;

namespace StudentOrganizer.Infrastructure.Services
{
	public class CourseService : ICourseService
	{
		private readonly IGroupRepository _groupRepository;
		private readonly IAdministratorService _administratorService;
		private readonly IMapper _mapper;

		public CourseService(IGroupRepository groupRepository, IAdministratorService administratorService, IMapper mapper)
		{
			_groupRepository = groupRepository;
			_administratorService = administratorService;
			_mapper = mapper;
		}

		public async Task AddCourses(AddCourses command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithCoursesAsync(command.GroupId);

			var courses = _mapper.Map<List<Course>>(command.Courses);
			group.AddCourses(courses);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task DeleteCourse(DeleteCourse command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithCoursesAsync(command.GroupId);
			
			group.DeleteCourse(command.CourseId);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task UpdateCourse(UpdateCourse command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await _groupRepository.GetWithCoursesAsync(command.GroupId);

			var course = _mapper.Map<Course>(command.Course);			
			group.UpdateCourse(course);
			
			await _groupRepository.SaveChangesAsync();
		}
	}
}