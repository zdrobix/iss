namespace PharmacyApi.Models.Domain
{
	public class OrderEntityContainer : Entity<int>
	{
		public ICollection<Order> Orders { get; set; } = new List<Order>();

		public OrderEntityContainer()
		{
		}
	}
}
