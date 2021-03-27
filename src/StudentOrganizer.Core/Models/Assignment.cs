using System;

namespace StudentOrganizer.Core.Models
{
	public class Assignment : Entity
	{
        public string Name { get; protected set; }
		public string Description { get; protected set; }
		public DateTime? Deadline { get; protected set; }
		public int Semester { get; protected set; }

        protected Assignment() { }

        public Assignment(string name, string description, int semester, DateTime deadline)
        {
            SetName(name);
            SetDescription(description);
            SetSemester(semester);
            SetDeadline(deadline);
        }

		public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name can not be empty.");
            }
            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception("Description can not be empty.");
            }
            Description = description;
        }

        public void SetSemester(int semester)
        {
            if(semester < 0)
            {
                throw new Exception("Semester can not be lower than zero.");
            }
            Semester = semester;
        }

        public void SetDeadline(DateTime deadline)
        {
            Deadline = deadline;
        }
    }
}