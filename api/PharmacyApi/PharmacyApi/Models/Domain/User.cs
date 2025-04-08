namespace PharmacyApi.Models.Domain
{
	public class User : Entity<int> 
	{
		public string Name { get; set; }
		public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

		public User (string name, string username, string password, string role)
		{
			Name = name;
			Username = username;
			Password = password;
			Role = role;
		}
	}
}
