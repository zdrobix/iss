namespace PharmacyApi.Models.DTO
{
	public class PharmacyDTO 
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DrugStorageDTO Storage { get; set; }
	}
}
