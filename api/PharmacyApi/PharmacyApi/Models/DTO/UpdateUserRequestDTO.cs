namespace PharmacyApi.Models.DTO
{
	public class UpdateUserRequestDTO
	{
		public string Name { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public PharmacyDTO? Pharmacy { get; set; }
		public HospitalDTO? Hospital { get; set; }
	}
}
