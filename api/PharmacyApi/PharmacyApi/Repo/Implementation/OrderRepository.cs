using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext dbContext;

		public OrderRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Order> CreateAsync(Order Order)
		{
			await dbContext.Orders.AddAsync(Order);
			await dbContext.SaveChangesAsync();
			return Order;
		}

		public async Task<IEnumerable<Order>> GetAllAsync() =>
			await dbContext.Orders
				.Include(po => po.OrderedDrugs)
					.ThenInclude(od => od.Drug)
				.Include(po => po.PlacedBy)
					.ThenInclude(po => po.Hospital)
				.Include(po => po.ResolvedBy)
					.ThenInclude(po => po.Pharmacy)
				.ToListAsync();

		public async Task<Order?> GetById(int id) =>
			await dbContext.Orders
				.Include(po => po.OrderedDrugs)
					.ThenInclude(od => od.Drug)
				.Include(po => po.PlacedBy)
					.ThenInclude(po => po.Hospital)
				.Include(po => po.ResolvedBy)
					.ThenInclude(po => po.Pharmacy)
				.FirstOrDefaultAsync(po => po.Id == id);

		public async Task<Order?> UpdateAsync(Order Order)
		{
			var existing = await dbContext.Orders
				.Include(po => po.OrderedDrugs)
					.ThenInclude(od => od.Drug)
				.Include(po => po.PlacedBy)
					.ThenInclude(po => po.Hospital)
				.Include(po => po.ResolvedBy)
					.ThenInclude(po => po.Pharmacy)
				.FirstOrDefaultAsync(po => po.Id == Order.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(Order);

			existing.OrderedDrugs = Order.OrderedDrugs;
			existing.PlacedBy = Order.PlacedBy;
			existing.ResolvedBy = Order.ResolvedBy;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<Order> DeleteAsync(int id)
		{
			var Order = await dbContext.Orders.FirstOrDefaultAsync(po => po.Id == id);

			if (Order == null)
				throw new InvalidOperationException();

			dbContext.Orders.Remove(Order);
			await dbContext.SaveChangesAsync();
			return Order;
		}
	}

}
