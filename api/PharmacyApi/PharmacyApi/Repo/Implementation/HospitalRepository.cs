	using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Repo.Implementation
{
	public class HospitalRepository : IHospitalRepository
	{
		private readonly ApplicationDbContext dbContext;

		public HospitalRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Hospital> CreateAsync(Hospital hospital)
		{
			await dbContext.Hospitals.AddAsync(hospital);
			await dbContext.SaveChangesAsync();
			return hospital;
		}

		public async Task<IEnumerable<Hospital>> GetAllAsync() =>
			await dbContext.Hospitals
				.Include(h => h.OrderContainer)
				.ToListAsync();

		public async Task<Hospital?> GetById(int id) =>
			await dbContext.Hospitals
				.Include(h => h.OrderContainer)
				.FirstOrDefaultAsync(h => h.Id == id);

		public async Task<Hospital?> UpdateAsync(Hospital hospital)
		{
			var existing = await dbContext.Hospitals
				.Include(h => h.OrderContainer)
				.FirstOrDefaultAsync(h => h.Id == hospital.Id);

			if (existing == null)
				return null;

			dbContext.Entry(existing).CurrentValues.SetValues(hospital);

			existing.OrderContainer = hospital.OrderContainer;

			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<Hospital> DeleteAsync(int id)
		{
			var hospital = await dbContext.Hospitals
				.FirstOrDefaultAsync(h => h.Id == id);

			if (hospital == null)
				throw new InvalidOperationException();

			dbContext.Hospitals.Remove(hospital);
			await dbContext.SaveChangesAsync();
			return hospital;
		}
	}

}
