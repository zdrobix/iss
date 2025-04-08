using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IResolvedOrderRepository
	{
		Task<ResolvedOrder> CreateAsync(ResolvedOrder resolvedOrder);
		Task<IEnumerable<ResolvedOrder>> GetAllAsync();
		Task<ResolvedOrder?> GetById(int id);
		Task<ResolvedOrder?> UpdateAsync(ResolvedOrder resolvedOrder);
		Task<ResolvedOrder> DeleteAsync(int id);
	}
}
