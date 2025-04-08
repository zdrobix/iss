namespace PharmacyApi.Models.Domain
{
	public class LoginEntity : Entity<int> 
	{
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginEntity (string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
