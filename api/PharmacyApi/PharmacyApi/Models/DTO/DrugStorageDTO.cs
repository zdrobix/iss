namespace PharmacyApi.Models.DTO
{
	public class DrugStorageDTO 
	{
        public ICollection<StoredDrugDTO> StoredDrugs { get; set; } = new List<StoredDrugDTO>();
	}
}
