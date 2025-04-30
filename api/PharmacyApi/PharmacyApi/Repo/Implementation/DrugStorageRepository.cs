using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class DrugStorageRepository : IDrugStorageRepository
	{
		private readonly ApplicationDbContext dbContext;

		public DrugStorageRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<DrugStorage> CreateAsync(DrugStorage drugStorage)
		{
			await dbContext.DrugStorages.AddAsync(drugStorage);
			await dbContext.SaveChangesAsync();
			return drugStorage;
		}

		public async Task<IEnumerable<DrugStorage>> GetAllAsync() =>
			await dbContext.DrugStorages
				.Include(ds => ds.StoredDrugs)
					.ThenInclude(sd => sd.Drug)
				.ToListAsync();

		public async Task<DrugStorage?> GetById(int id) =>
			await dbContext.DrugStorages
				.Include(ds => ds.StoredDrugs)
					.ThenInclude(sd => sd.Drug)
				.FirstOrDefaultAsync(ds => ds.Id == id);

		public async Task<DrugStorage?> UpdateAsync(DrugStorage drugStorage)
		{
			var existing = await dbContext.DrugStorages
				.Include(ds => ds.StoredDrugs)
					.ThenInclude(sd => sd.Drug)
				.FirstOrDefaultAsync(ds => ds.Id == drugStorage.Id);

			if (existing == null)
				return null;

			dbContext.StoredDrugs.RemoveRange(existing.StoredDrugs);

			existing.StoredDrugs = drugStorage.StoredDrugs;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<DrugStorage> DeleteAsync(int id)
		{
			var storage = await dbContext.DrugStorages
				.FirstOrDefaultAsync(ds => ds.Id == id);

			if (storage == null)
				throw new InvalidOperationException();

			dbContext.DrugStorages.Remove(storage);
			await dbContext.SaveChangesAsync();
			return storage;
		}
	}

}
