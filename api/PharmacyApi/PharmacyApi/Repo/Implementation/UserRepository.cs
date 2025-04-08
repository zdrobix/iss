using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Repo.Interface;

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

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await dbContext.Users.ToListAsync();
		}

		public async Task<User?> GetById(int id)
		{
			return await dbContext.Users.FindAsync(id);
		}

		public async Task<User?> GetByUsername(string username)
		{
			return await dbContext.Users
				.FirstOrDefaultAsync(u => u.Username == username);
		}

		public async Task<User?> UpdateAsync(User user)
		{
			var existingUser = await dbContext.Users.FindAsync(user.Id);
			if (existingUser == null)
			{
				return null;
			}

			dbContext.Entry(existingUser).CurrentValues.SetValues(user);
			await dbContext.SaveChangesAsync();
			return existingUser;
		}
	}

}
