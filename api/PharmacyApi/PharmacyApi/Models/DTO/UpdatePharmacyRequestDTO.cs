namespace PharmacyApi.Models.DTO
{
	public class UpdatePharmacyRequestDTO
	{
		public string Name { get; set; }
		public DrugStorageDTO Storage { get; set; }
	}
}
