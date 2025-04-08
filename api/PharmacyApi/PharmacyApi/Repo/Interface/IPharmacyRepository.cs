using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IPharmacyRepository
	{
		Task<Pharmacy> CreateAsync(Pharmacy pharmacy);
		Task<IEnumerable<Pharmacy>> GetAllAsync();
		Task<Pharmacy?> GetById(int id);
		Task<Pharmacy?> UpdateAsync(Pharmacy pharmacy);
		Task<Pharmacy> DeleteAsync(int id);
	}
}
