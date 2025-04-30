using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class DrugRepository : IDrugRepository
	{
		private readonly ApplicationDbContext dbContext;

		public DrugRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Drug> CreateAsync(Drug drug)
		{
			await dbContext.Drugs.AddAsync(drug);
			await dbContext.SaveChangesAsync();
			return drug;
		}

		public Task<Drug> DeleteAsync(int id)
		{
			var deleted = dbContext.Drugs.FirstOrDefault(d => d.Id == id);
			if (deleted == null)
				throw new ArgumentNullException(nameof(deleted), "Drug not found");
			dbContext.Drugs.Remove(deleted);
			dbContext.SaveChanges();
			return Task.FromResult(deleted);
		}

		public async Task<IEnumerable<Drug>> GetAllAsync() =>
			await dbContext.Drugs.ToListAsync();

		public async Task<Drug?> GetById(int id)
		{
			var drug = await dbContext.Drugs.FindAsync(id);
			var entry = dbContext.Entry(drug);
			if (entry.State == EntityState.Detached)
			{
				this.dbContext.Attach(drug); 
			}
			return drug;
		}

		public Task<Drug?> UpdateAsync(Drug drug)
		{
			throw new NotImplementedException();
		}
	}
}
