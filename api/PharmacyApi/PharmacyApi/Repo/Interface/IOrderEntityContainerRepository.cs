using PharmacyApi.Models.Domain;

namespace PharmacyApi.Repo.Interface
{
	public interface IOrderEntityContainerRepository
	{
		Task<OrderEntityContainer> CreateAsync(OrderEntityContainer orderContainer);
		Task<IEnumerable<OrderEntityContainer>> GetAllAsync();
		Task<OrderEntityContainer?> GetById(int id);
		Task<OrderEntityContainer?> UpdateAsync(OrderEntityContainer orderContainer);
		Task<OrderEntityContainer> DeleteAsync(int id);
	}
}
