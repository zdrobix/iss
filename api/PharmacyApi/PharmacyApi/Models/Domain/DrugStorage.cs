namespace PharmacyApi.Models.Domain
{
	public class DrugStorage : Entity<int> 
	{
		public ICollection<StoredDrug> StoredDrugs { get; set; } = new List<StoredDrug>();

		public DrugStorage()
		{
			StoredDrugs = new List<StoredDrug>();
		}

		public DrugStorage(ICollection<StoredDrug> storedDrugs)
		{
			StoredDrugs = storedDrugs;
		}
	}
}
