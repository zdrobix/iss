using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class PlacedOrderRepository : IPlacedOrderRepository
	{
		private readonly ApplicationDbContext dbContext;

		public PlacedOrderRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<PlacedOrder> CreateAsync(PlacedOrder placedOrder)
		{
			await dbContext.PlacedOrders.AddAsync(placedOrder);
			await dbContext.SaveChangesAsync();
			return placedOrder;
		}

		public async Task<IEnumerable<PlacedOrder>> GetAllAsync() =>
			await dbContext.PlacedOrders
				.Include(po => po.OrderedDrugs)
				.Include(po => po.PlacedBy)
				.ToListAsync();

		public async Task<PlacedOrder?> GetById(int id) =>
			await dbContext.PlacedOrders
				.Include(po => po.OrderedDrugs)
				.Include(po => po.PlacedBy)
				.FirstOrDefaultAsync(po => po.Id == id);

		public async Task<PlacedOrder?> UpdateAsync(PlacedOrder placedOrder)
		{
			var existing = await dbContext.PlacedOrders
				.Include(po => po.OrderedDrugs)
				.Include(po => po.PlacedBy)
				.FirstOrDefaultAsync(po => po.Id == placedOrder.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(placedOrder);

			existing.OrderedDrugs = placedOrder.OrderedDrugs;
			existing.PlacedBy = placedOrder.PlacedBy;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<PlacedOrder> DeleteAsync(int id)
		{
			var placedOrder = await dbContext.PlacedOrders.FirstOrDefaultAsync(po => po.Id == id);

			if (placedOrder == null)
				throw new InvalidOperationException();

			dbContext.PlacedOrders.Remove(placedOrder);
			await dbContext.SaveChangesAsync();
			return placedOrder;
		}
	}

}
