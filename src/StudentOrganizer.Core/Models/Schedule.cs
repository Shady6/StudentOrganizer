using System;
using System.Collections.Generic;
using StudentOrganizer.Core.Common;

namespace StudentOrganizer.Core.Models
{
	public class Schedule : Entity
	{
		public IList<ScheduledCourse> ScheduledCourses { get; protected set; }
		public int Semester { get; protected set; }

		public Schedule()
		{

		}

        public Schedule(int semester, IList<ScheduledCourse> courses)
        {
			Semester = semester;
			ScheduledCourses = courses;
        }

		public void Update(int semester, IList<ScheduledCourse> courses)
		{
			Semester = semester;
			ScheduledCourses = courses;
		}

		public void SetSemester(int semester)
        {
			if (semester < 0)
            {
				throw new AppException("Semester can not be lower than zero.", AppErrorCode.VALIDATION_ERROR);
            }
			Semester = semester;
        }
	}
}