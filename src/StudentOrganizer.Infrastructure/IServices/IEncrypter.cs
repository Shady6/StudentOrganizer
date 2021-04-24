namespace StudentOrganizer.Infrastructure.IServices
{
	public interface IEncrypter : IService
	{
		public string GetHash(string password, string salt);

		public string GetSalt(string password);
	}
}