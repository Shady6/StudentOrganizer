namespace StudentOrganizer.Core.Models
{
	public class Team : Entity
	{
		public string Group { get; set; }
		public Schedule Schedule { get; set; }
	}
}