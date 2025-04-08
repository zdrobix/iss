using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IDrugRepository
	{
		Task<Drug> CreateAsync (Drug drug);
		Task<IEnumerable<Drug>> GetAllAsync ();
		Task<Drug?> GetById(int id);
		Task<Drug?> UpdateAsync(Drug drug);
		Task<Drug> DeleteAsync(int id);
	}
}
