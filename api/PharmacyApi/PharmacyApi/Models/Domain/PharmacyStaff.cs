namespace PharmacyApi.Models.Domain
{
	public class PharmacyStaff : LoginEntity
	{
        public string Name { get; set; }

		public PharmacyStaff (string name, string username, string password) : base(username, password)
		{
			this.Name = name;
		}

	}
}
