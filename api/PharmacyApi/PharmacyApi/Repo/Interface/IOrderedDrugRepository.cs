using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IOrderedDrugRepository
	{
		Task<OrderedDrug> CreateAsync(OrderedDrug orderedDrug);
		Task<IEnumerable<OrderedDrug>> GetAllAsync();
		Task<OrderedDrug?> GetById(int id);
		Task<OrderedDrug?> UpdateAsync(OrderedDrug orderedDrug);
		Task<OrderedDrug> DeleteAsync(int id);
	}
}
