using StudentOrganizer.Core.Behaviors.Assignments;
using StudentOrganizer.Core.Common;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Commands.Assignments;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Services
{
	public class AssignmentService : IAssignmentService
	{
		private readonly IAdministratorService _administratorService;
		private readonly IGroupRepository _groupRepository;

		public AssignmentService(IAdministratorService administratorService, IGroupRepository groupRepository)
		{
			_administratorService = administratorService;
			_groupRepository = groupRepository;
		}
		
		public async Task AddAssignment(AddAssignment command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await GetGroupForAssignmentActions(command);
			var course = command.CourseId == null ? null : ValidateCourseExists(command.CourseId.Value, group);
			IAssignmentActions assignmentActions =
				command is ITeamAssignment cmd ? group.Teams.First(t => t.Name == cmd.TeamName) : group;

			assignmentActions.AddAsignment(new Assignment(
				command.Name,
				command.Description,
				course == null ? command.Semester : course.Semester,
				command.Deadline,
				course));

			await _groupRepository.SaveChangesAsync();
		}

		public async Task UpdateAssignment(UpdateAssignment command)
		{
			await _administratorService.ValidateAtLeastModerator(command.UserId, command.GroupId);
			var group = await GetGroupForAssignmentActions(command);
			var course = command.CourseId == null ? null : ValidateCourseExists(command.CourseId.Value, group);
			IAssignmentActions assignmentActions =
				command is ITeamAssignment cmd ? group.Teams.First(t => t.Name == cmd.TeamName) : group;

			assignmentActions.UpdateAssignment(command.Name,
				command.Description,
				course == null ? command.Semester : course.Semester,
				command.Deadline,
				course,
				command.AssignmentId);

			await _groupRepository.SaveChangesAsync();
		}

		public async Task DeleteAssignment(DeleteAssignment command)
		{
			await _administratorService.ValidateAtLeastAdministrator(command.UserId, command.GroupId);
			var group = await GetGroupForAssignmentActions(command);
			IAssignmentActions assignmentActions =
				command is ITeamAssignment cmd ? group.Teams.First(t => t.Name == cmd.TeamName) : group;

			assignmentActions.DeleteAssignment(command.AssignmentId);

			await _groupRepository.SaveChangesAsync();
		}

		private Course ValidateCourseExists(Guid courseId, Group group)
		{			
			var course = group.Courses.FirstOrDefault(c => c.Id == courseId);
			if (course == null)
				throw new AppException($"You're trying to add an assignment which has non existing course id", AppErrorCode.DOESNT_EXIST);
			return course;
		}

		private async Task<Group> GetGroupForAssignmentActions(IGroupIdentifier command)
		{
			Group group;
			if (command is ITeamAssignment cmd)
			{
				group = await _groupRepository.GetWithTeamAssignmentsAsync(command.GroupId);
				var team = group.Teams.FirstOrDefault(t => t.Name == cmd.TeamName);
				if (team == null)
					throw new AppException($"The team with name {cmd.TeamName} doesn't exist in the group", AppErrorCode.DOESNT_EXIST);
			}
			else
				group = await _groupRepository.GetWithAssignments(command.GroupId);

			return group;
		}
	}
}