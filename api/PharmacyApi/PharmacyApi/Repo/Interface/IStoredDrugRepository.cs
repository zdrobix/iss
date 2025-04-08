using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IStoredDrugRepository
	{
		Task<StoredDrug> CreateAsync(StoredDrug storedDrug);
		Task<IEnumerable<StoredDrug>> GetAllAsync();
		Task<StoredDrug?> GetById(int id);
		Task<StoredDrug?> UpdateAsync(StoredDrug storedDrug);
		Task<StoredDrug> DeleteAsync(int id);
	}
}
