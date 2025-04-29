using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IOrderRepository
	{
		Task<Order> CreateAsync(Order order);
		Task<IEnumerable<Order>> GetAllAsync();
		Task<Order?> GetById(int id);
		Task<Order?> UpdateAsync(Order order);
		Task<Order> DeleteAsync(int id);
	}
}
