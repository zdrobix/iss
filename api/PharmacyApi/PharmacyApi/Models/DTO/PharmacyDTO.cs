namespace PharmacyApi.Models.DTO
{
	public class PharmacyDTO 
	{
		public string Name { get; set; }
		public ICollection<UserDTO> Staff { get; set; }
		public DrugStorageDTO Storage { get; set; }
	}
}
