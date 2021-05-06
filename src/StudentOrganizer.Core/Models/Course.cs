using System;
using StudentOrganizer.Core.Common;

namespace StudentOrganizer.Core.Models
{
	public class Course : Entity
	{
		public string Name { get; protected set; }
		public string Lecturer { get; protected set; }
		public Location Location { get; protected set; }
		public int Semester { get; protected set; }

		public Course(string name, string lecturer, Location location, int semester)
		{
			SetName(name);
			SetLecturer(lecturer);
			Location = location;
			SetSemester(semester);
		}

		public Course(Guid id)
		{
			Id = id;
		}

		public Course()
		{
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new AppException("Name can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			Name = name;
		}

		public void SetLecturer(string lecturer)
		{
			if (string.IsNullOrWhiteSpace(lecturer))
			{
				throw new AppException("Lecturer can not be empty.", AppErrorCode.VALIDATION_ERROR);
			}
			Lecturer = lecturer;
		}

		public void SetSemester(int semester)
		{
			if (semester < 0)
			{
				throw new AppException("Semester can not be lower than zero.", AppErrorCode.VALIDATION_ERROR);
			}
			Semester = semester;
		}

		public void Update(Course course)
		{
			SetName(course.Name);
			SetLecturer(course.Lecturer);
			Location = course.Location;
			SetSemester(course.Semester);
		}

		public override bool Equals(object obj)
		{
			var other = obj as Course;

			return Name == other.Name &&
				Lecturer == other.Lecturer &&
				Semester == other.Semester &&
				Location.Equals(other.Location);
		}
	}
}