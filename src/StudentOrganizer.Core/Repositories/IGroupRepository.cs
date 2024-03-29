﻿using System;
using System.Linq;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;

namespace StudentOrganizer.Core.Repositories
{
	public interface IGroupRepository
	{
		Task AddAsync(Group group);

		Task<Group> GetAsync(Guid id);

		Task<Group> GetAsync(string name);

		void Delete(Guid id);

		void Update(Group group);

		IQueryable<Group> GetAll();

		Task<Group> GetWholeGroupAsync(Guid id);

		Task SaveChangesAsync();

		Task<Group> GetWithCoursesAsync(Guid id);

		Task<Group> GetWithTeamsAsync(Guid id);

		Task<Group> GetWithTeamScheduleAndCourses(Guid id, string teamName);

		Task<Group> GetWithStudents(Guid GroupId);

		Task<Group> GetWithGroupAndTeamStudents(Guid id);

		IQueryable<Group> GetWholeGroupsAsync(Guid userId);

		Task<Group> GetWithAllUsers(Guid groupId);
		Task<Group> GetWithTeamStudentsAsync(Guid id);
		Task<Group> GetWithTeamAssignmentsAsync(Guid id);
		Task<Group> GetWithAssignments(Guid GroupId);
	}
}