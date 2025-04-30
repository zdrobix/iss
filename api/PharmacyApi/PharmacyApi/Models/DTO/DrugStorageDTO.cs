namespace PharmacyApi.Models.DTO
{
	public class DrugStorageDTO 
	{
		public int Id { get; set; }
		public ICollection<StoredDrugDTO> StoredDrugs { get; set; } = new List<StoredDrugDTO>();
	}
}
