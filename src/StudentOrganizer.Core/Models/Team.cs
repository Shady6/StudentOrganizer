namespace StudentOrganizer.Core.Models
{
	public class Team : Entity
	{
		public Schedule Schedule { get; protected set; }
		public Team()
        {

        }

        public Team(Schedule schedule)
        {
			Schedule = schedule;
        }
	}
}