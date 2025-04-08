using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PharmacyApi.Models.Domain
{
	public class StoredDrug : Entity<int>
	{
		public int Quantity { get; set; }
		public Drug Drug { get; set; }

		public StoredDrug (int quantity, Drug drug)
		{
			Quantity = quantity;
			Drug = drug;
		}

		public StoredDrug() { }
	}
}
