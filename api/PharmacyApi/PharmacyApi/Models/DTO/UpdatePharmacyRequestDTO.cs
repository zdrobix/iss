namespace PharmacyApi.Models.DTO
{
	public class UpdatePharmacyRequestDTO
	{
		public string Name { get; set; }
		public ICollection<UserDTO> Staff { get; set; }
		public DrugStorageDTO Storage { get; set; }
	}
}
