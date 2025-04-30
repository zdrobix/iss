namespace PharmacyApi.Models.DTO
{
	public class UpdateDrugStorageRequestDTO
	{
		public ICollection<StoredDrugDTO> StoredDrugs { get; set; } = new List<StoredDrugDTO>();
	}
}
