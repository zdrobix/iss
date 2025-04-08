namespace PharmacyApi.Models.DTO
{
	public class HospitalDTO 
	{
		public string Name { get; set; }
		public ICollection<UserDTO> Staff { get; set; }
	}
}
