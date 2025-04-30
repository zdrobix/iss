using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;
using Serilog;

namespace PharmacyApi.Repo.Implementation
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext dbContext;

		public UserRepository(ApplicationDbContext context)
		{
			dbContext = context;
		}

		public async Task<User> CreateAsync(User user)
		{
			await dbContext.Users.AddAsync(user);
			await dbContext.SaveChangesAsync();
			return user;
		}

		public async Task<User> DeleteAsync(int id)
		{
			var user = await dbContext.Users.FindAsync(id);
			if (user == null)
			{
				throw new KeyNotFoundException($"User with id {id} not found.");
			}

			dbContext.Users.Remove(user);
			await dbContext.SaveChangesAsync();
			return user;
		}

		public async Task<IEnumerable<User>> GetAllAsync() =>
			await dbContext.Users
					.Include(u => u.Hospital)
					.Include(u => u.Pharmacy)
						.ThenInclude(p => p.Storage)
							.ThenInclude(s => s.StoredDrugs)
								.ThenInclude(s => s.Drug)
					.ToListAsync();

		public async Task<User?> GetById(int id) =>
			await dbContext.Users
						.Include(u => u.Hospital)
						.Include(u => u.Pharmacy)
							.ThenInclude(p => p.Storage)
								.ThenInclude(s => s.StoredDrugs)
									.ThenInclude(s => s.Drug)
						.FirstOrDefaultAsync(u => u.Id == id);

		public async Task<User?> GetByUsername(string username) =>
			await dbContext.Users
				.Include(u => u.Hospital)
				.Include(u => u.Pharmacy)
					.ThenInclude(p => p.Storage)
							.ThenInclude(s => s.StoredDrugs)
								.ThenInclude(s => s.Drug)
				.FirstOrDefaultAsync(u => u.Username == username);

		public async Task<User?> UpdateAsync(User user)
		{
			Log.Information($"Updating user {user.Name}");
			var existingUser = await dbContext.Users
								.Include(u => u.Hospital)
								.Include(u => u.Pharmacy)
									.ThenInclude(p => p.Storage)
										.ThenInclude(s => s.StoredDrugs)
											.ThenInclude(s => s.Drug)
									.FirstOrDefaultAsync(u => u.Id == user.Id);
			if (existingUser == null)
				return null;

			dbContext.Entry(existingUser).CurrentValues.SetValues(user);
			await dbContext.SaveChangesAsync();
			return existingUser;
		}
	}

}
