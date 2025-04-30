using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PharmacyApi.Models.Domain
{
	public class StoredDrug : Entity<int>
	{
		public int Quantity { get; set; }
		public Drug Drug { get; set; }
		public int DrugStorageId { get; set; }
		public DrugStorage Storage { get; set; }


		public StoredDrug (int quantity, Drug drug, DrugStorage storage)
		{
			Quantity = quantity;
			Drug = drug;
			Storage = storage;
		}

		public StoredDrug() {
			Drug = new Drug();
			Storage = new DrugStorage();
		}
	}
}
