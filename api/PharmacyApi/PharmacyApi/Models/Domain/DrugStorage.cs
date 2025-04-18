﻿namespace PharmacyApi.Models.Domain
{
	public class DrugStorage : Entity<int>
	{
        public ICollection<StoredDrug> StoredDrugs { get; set; }

		public DrugStorage(ICollection<StoredDrug> storedDrugs)
		{
			StoredDrugs = storedDrugs;
		}

		public DrugStorage()
		{
			StoredDrugs = new List<StoredDrug>();
		}
	}
}
