using System;
using System.Collections.Generic;

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
				throw new Exception("Semester can not be lower than zero.");
            }
			Semester = semester;
        }
	}
}