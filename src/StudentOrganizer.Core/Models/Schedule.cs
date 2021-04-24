using System;
using System.Collections.Generic;

namespace StudentOrganizer.Core.Models
{
	public class Schedule : Entity
	{
		public ISet<ScheduledCourse> _scheduledCourses = new HashSet<ScheduledCourse>();
		public IEnumerable<ScheduledCourse> ScheduledCourse => _scheduledCourses;
		public int Semester { get; protected set; }

        public Schedule(int semester)
        {
			Semester = semester;
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