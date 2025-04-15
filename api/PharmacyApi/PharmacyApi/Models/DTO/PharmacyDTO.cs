namespace PharmacyApi.Models.DTO
{
	public class PharmacyDTO 
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<UserDTO> Staff { get; set; }
		public DrugStorageDTO Storage { get; set; }
	}
}
