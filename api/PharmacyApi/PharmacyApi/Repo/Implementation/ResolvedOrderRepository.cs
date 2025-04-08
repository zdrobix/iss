using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class ResolvedOrderRepository : IResolvedOrderRepository
	{
		private readonly ApplicationDbContext dbContext;

		public ResolvedOrderRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<ResolvedOrder> CreateAsync(ResolvedOrder resolvedOrder)
		{
			await dbContext.ResolvedOrders.AddAsync(resolvedOrder);
			await dbContext.SaveChangesAsync();
			return resolvedOrder;
		}

		public async Task<IEnumerable<ResolvedOrder>> GetAllAsync() =>
			await dbContext.ResolvedOrders
				.Include(ro => ro.OrderedDrugs)
				.Include(ro => ro.ResolvedBy)
				.ToListAsync();

		public async Task<ResolvedOrder?> GetById(int id) =>
			await dbContext.ResolvedOrders
				.Include(ro => ro.OrderedDrugs)
				.Include(ro => ro.ResolvedBy)
				.FirstOrDefaultAsync(ro => ro.Id == id);

		public async Task<ResolvedOrder?> UpdateAsync(ResolvedOrder resolvedOrder)
		{
			var existing = await dbContext.ResolvedOrders
				.Include(ro => ro.OrderedDrugs)
				.Include(ro => ro.ResolvedBy)
				.FirstOrDefaultAsync(ro => ro.Id == resolvedOrder.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(resolvedOrder);

			existing.OrderedDrugs = resolvedOrder.OrderedDrugs;
			existing.ResolvedBy = resolvedOrder.ResolvedBy;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<ResolvedOrder> DeleteAsync(int id)
		{
			var resolvedOrder = await dbContext.ResolvedOrders.FirstOrDefaultAsync(ro => ro.Id == id);

			if (resolvedOrder == null)
				throw new InvalidOperationException();

			dbContext.ResolvedOrders.Remove(resolvedOrder);
			await dbContext.SaveChangesAsync();
			return resolvedOrder;
		}
	}

}
