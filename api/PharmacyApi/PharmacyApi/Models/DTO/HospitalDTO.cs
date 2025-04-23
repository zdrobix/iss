namespace PharmacyApi.Models.DTO
{
	public class HospitalDTO 
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<UserDTO> Staff { get; set; }
	}
}
