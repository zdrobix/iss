namespace PharmacyApi.Models.Domain
{
	public class OrderedDrug : Entity<int>
	{
		public int Quantity { get; set; }
		public Drug Drug { get; set; }

		public OrderedDrug(int quantity, Drug drug)
		{
			Quantity = quantity;
			Drug = drug;
		}

		public OrderedDrug()
		{

		}
	}
}
