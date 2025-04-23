namespace PharmacyApi.Models.DTO
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public int? PharmacyId { get; set; }
		public int? HospitalId { get; set; }
		public PharmacyDTO? Pharmacy { get; set; }
		public HospitalDTO? Hospital { get; set; }
	}
}
