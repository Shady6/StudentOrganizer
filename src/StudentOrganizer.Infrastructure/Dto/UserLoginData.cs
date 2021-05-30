namespace StudentOrganizer.Infrastructure.Dto
{
	public class UserLoginData
	{
		public JwtDto Token { get; set; }
		public LoginUser User { get; set; }		
	}	

}