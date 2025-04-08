using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class OrderedDrugRepository : IOrderedDrugRepository
	{
		private readonly ApplicationDbContext dbContext;

		public OrderedDrugRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<OrderedDrug> CreateAsync(OrderedDrug orderedDrug)
		{
			await dbContext.OrderedDrugs.AddAsync(orderedDrug);
			await dbContext.SaveChangesAsync();
			return orderedDrug;
		}

		public async Task<IEnumerable<OrderedDrug>> GetAllAsync() =>
			await dbContext.OrderedDrugs
				.Include(od => od.Drug)
				.ToListAsync();

		public async Task<OrderedDrug?> GetById(int id) =>
			await dbContext.OrderedDrugs
				.Include(od => od.Drug)
				.FirstOrDefaultAsync(od => od.Id == id);

		public async Task<OrderedDrug?> UpdateAsync(OrderedDrug orderedDrug)
		{
			var existing = await dbContext.OrderedDrugs
				.Include(od => od.Drug)
				.FirstOrDefaultAsync(od => od.Id == orderedDrug.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(orderedDrug);
			existing.Drug = orderedDrug.Drug;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<OrderedDrug> DeleteAsync(int id)
		{
			var orderedDrug = await dbContext.OrderedDrugs.FirstOrDefaultAsync(od => od.Id == id);

			if (orderedDrug == null)
				throw new InvalidOperationException();

			dbContext.OrderedDrugs.Remove(orderedDrug);
			await dbContext.SaveChangesAsync();
			return orderedDrug;
		}
	}

}
