namespace PharmacyApi.Models.DTO
{
	public class OrderedDrugDTO
	{
		public int Quantity { get; set; }
		public DrugDTO Drug { get; set; }
	}
}
