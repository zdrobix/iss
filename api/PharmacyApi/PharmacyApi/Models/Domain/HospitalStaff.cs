namespace PharmacyApi.Models.Domain
{
	public class HospitalStaff : LoginEntity
	{
		public string Name { get; set; }

		public HospitalStaff(string name, string username, string password) : base(username, password)
		{
			this.Name = name;
		}
	}
}
