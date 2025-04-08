using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IHospitalRepository
	{
		Task<Hospital> CreateAsync(Hospital hospital);
		Task<IEnumerable<Hospital>> GetAllAsync();
		Task<Hospital?> GetById(int id);
		Task<Hospital?> UpdateAsync(Hospital hospital);
		Task<Hospital> DeleteAsync(int id);
	}
}
