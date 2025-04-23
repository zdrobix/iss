namespace PharmacyApi.Models.Domain
{
	public class OrderEntityContainer : Entity<int>
	{
		public ICollection<PlacedOrder> PlacedOrders { get; set; } = new List<PlacedOrder>();
		public ICollection<ResolvedOrder> ResolvedOrders { get; set; } = new List<ResolvedOrder>();

		public OrderEntityContainer(ICollection<PlacedOrder> placedOrders, ICollection<ResolvedOrder> resolvedOrders)
		{
			PlacedOrders = placedOrders;
			ResolvedOrders = resolvedOrders;
		}
	}
}
