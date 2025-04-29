using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models.Domain;

namespace PharmacyApi.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Drug> Drugs { get; set; }
		public DbSet<OrderedDrug> OrderedDrugs { get; set; }
		public DbSet<StoredDrug> StoredDrugs { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Pharmacy> Pharmacies { get; set; }
		public DbSet<Hospital> Hospitals { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<DrugStorage> DrugStorages { get; set; }
		public DbSet<OrderEntityContainer> OrderContainers { get; set; }
	}
}
