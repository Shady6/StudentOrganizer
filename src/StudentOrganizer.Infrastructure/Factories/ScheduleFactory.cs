using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Infrastructure.Commands.Schedules;
using StudentOrganizer.Infrastructure.Dto;

namespace StudentOrganizer.Infrastructure.Factories
{
	public class ScheduleFactory
	{
		public static Schedule Create(ScheduleDto scheduleDto, IEnumerable<Course> courses)
		{
			foreach (var scheduledCourse in scheduleDto.ScheduledCourses)
			{
				if (!courses.Any(c => c.Id == scheduledCourse.Course.Id))
					throw new Exception($"Course with id {scheduledCourse.Course.Id} doesn't exist in this group");
			}

			var scheduledCourses = scheduleDto.ScheduledCourses.Select(sc =>
			new ScheduledCourse(
				sc.DayOfTheWeek,
				sc.StartTime,
				sc.EndTime,
				courses.First(c => c.Id == sc.Course.Id)
				))
				.ToList();
				
			return new Schedule(scheduleDto.Semester, scheduledCourses);
		}
	}
}