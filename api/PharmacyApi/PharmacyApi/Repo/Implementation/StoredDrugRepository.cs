using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class StoredDrugRepository : IStoredDrugRepository
	{
		private readonly ApplicationDbContext dbContext;

		public StoredDrugRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<StoredDrug> CreateAsync(StoredDrug storedDrug)
		{
			await dbContext.StoredDrugs.AddAsync(storedDrug);
			await dbContext.SaveChangesAsync();
			return storedDrug;
		}

		public async Task<IEnumerable<StoredDrug>> GetAllAsync() =>
			await dbContext.StoredDrugs
				.Include(sd => sd.Drug)
				.Include(sd => sd.Storage)
				.ToListAsync();

		public async Task<StoredDrug?> GetById(int id) =>
			await dbContext.StoredDrugs
				.Include(sd => sd.Drug)
				.FirstOrDefaultAsync(sd => sd.Id == id);

		public async Task<StoredDrug?> GetByDrugIdAndStorageId(int drugId, int storageId) =>
			await dbContext.StoredDrugs
				.Include(sd => sd.Drug)
				.Include(sd => sd.Storage)
				.FirstOrDefaultAsync(sd => sd.Drug.Id == drugId && sd.Storage.Id == storageId);

		public async Task<StoredDrug?> UpdateAsync(StoredDrug storedDrug)
		{
			var existing = await dbContext.StoredDrugs
				.Include(sd => sd.Drug)
				.Include(sd => sd.Storage)
				.FirstOrDefaultAsync(sd => sd.Id == storedDrug.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(storedDrug);
			existing.Drug = storedDrug.Drug;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<StoredDrug> DeleteAsync(int id)
		{
			var storedDrug = await dbContext.StoredDrugs.FirstOrDefaultAsync(sd => sd.Id == id);

			if (storedDrug == null)
				throw new InvalidOperationException();

			dbContext.StoredDrugs.Remove(storedDrug);
			await dbContext.SaveChangesAsync();
			return storedDrug;
		}
	}

}
