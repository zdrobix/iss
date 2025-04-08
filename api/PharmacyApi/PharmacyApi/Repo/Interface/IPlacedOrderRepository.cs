using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IPlacedOrderRepository
	{
		Task<PlacedOrder> CreateAsync(PlacedOrder placedOrder);
		Task<IEnumerable<PlacedOrder>> GetAllAsync();
		Task<PlacedOrder?> GetById(int id);
		Task<PlacedOrder?> UpdateAsync(PlacedOrder placedOrder);
		Task<PlacedOrder> DeleteAsync(int id);
	}
}
