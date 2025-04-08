using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class PharmacyRepository : IPharmacyRepository
	{
		private readonly ApplicationDbContext dbContext;

		public PharmacyRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Pharmacy> CreateAsync(Pharmacy pharmacy)
		{
			await dbContext.Pharmacies.AddAsync(pharmacy);
			await dbContext.SaveChangesAsync();
			return pharmacy;
		}

		public async Task<IEnumerable<Pharmacy>> GetAllAsync() =>
			await dbContext.Pharmacies
				.Include(p => p.Staff)
				.Include(p => p.Storage)
				.Include(p => p.PlacedOrders)
				.Include(p => p.ResolvedOrders)
				.ToListAsync();

		public async Task<Pharmacy?> GetById(int id) =>
			await dbContext.Pharmacies
				.Include(p => p.Staff)
				.Include(p => p.Storage)
				.Include(p => p.PlacedOrders)
				.Include(p => p.ResolvedOrders)
				.FirstOrDefaultAsync(p => p.Id == id);

		public async Task<Pharmacy?> UpdateAsync(Pharmacy pharmacy)
		{
			var existing = await dbContext.Pharmacies
				.Include(p => p.Staff)
				.Include(p => p.Storage)
				.Include(p => p.PlacedOrders)
				.Include(p => p.ResolvedOrders)
				.FirstOrDefaultAsync(p => p.Id == pharmacy.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(pharmacy);

			existing.Staff = pharmacy.Staff;
			existing.Storage = pharmacy.Storage;
			existing.PlacedOrders = pharmacy.PlacedOrders;
			existing.ResolvedOrders = pharmacy.ResolvedOrders;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<Pharmacy> DeleteAsync(int id)
		{
			var pharmacy = await dbContext.Pharmacies.FirstOrDefaultAsync(p => p.Id == id);

			if (pharmacy == null)
				throw new InvalidOperationException();

			dbContext.Pharmacies.Remove(pharmacy);
			await dbContext.SaveChangesAsync();
			return pharmacy;
		}
	}

}
