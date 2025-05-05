namespace PharmacyApi.Models.DTO
{
	public class AddStoredDrugRequestDTO
	{
		public DrugDTO Drug { get; set; } 
		public int Quantity { get; set; }
		public int StorageId { get; set; }
	}
}
