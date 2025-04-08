using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IDrugStorageRepository
	{
		Task<DrugStorage> CreateAsync(DrugStorage drugStorage);
		Task<IEnumerable<DrugStorage>> GetAllAsync();
		Task<DrugStorage?> GetById(int id);
		Task<DrugStorage?> UpdateAsync(DrugStorage drugStorage);
		Task<DrugStorage> DeleteAsync(int id);
	}
}
