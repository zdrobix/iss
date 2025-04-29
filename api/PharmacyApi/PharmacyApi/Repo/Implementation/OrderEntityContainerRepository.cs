using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class OrderEntityContainerRepository : IOrderEntityContainerRepository
	{
		private readonly ApplicationDbContext dbContext;

		public OrderEntityContainerRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<OrderEntityContainer> CreateAsync(OrderEntityContainer orderContainer)
		{
			await dbContext.OrderContainers.AddAsync(orderContainer);
			await dbContext.SaveChangesAsync();
			return orderContainer;
		}

		public async Task<IEnumerable<OrderEntityContainer>> GetAllAsync() =>
			await dbContext.OrderContainers
				.Include(o => o.Orders)
				.ToListAsync();

		public async Task<OrderEntityContainer?> GetById(int id) =>
			await dbContext.OrderContainers
				.Include(o => o.Orders)
				.FirstOrDefaultAsync(ds => ds.Id == id);

		public async Task<OrderEntityContainer?> UpdateAsync(OrderEntityContainer orderContainer)
		{
			var existing = await dbContext.OrderContainers
				.Include(o => o.Orders)
				.FirstOrDefaultAsync(ds => ds.Id == orderContainer.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(orderContainer);
			existing.Orders = orderContainer.Orders;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<OrderEntityContainer> DeleteAsync(int id)
		{
			var storage = await dbContext.OrderContainers
				.FirstOrDefaultAsync(ds => ds.Id == id);

			if (storage == null)
				throw new InvalidOperationException();

			dbContext.OrderContainers.Remove(storage);
			await dbContext.SaveChangesAsync();
			return storage;
		}
	}

}
