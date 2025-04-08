namespace PharmacyApi.Models.Domain
{
	public class OrderEntityContainer : Entity<int>
	{
		public ICollection<PlacedOrder> PlacedOrders { get; set; }
		public ICollection<ResolvedOrder> ResolvedOrders { get; set; }

		public OrderEntityContainer(ICollection<PlacedOrder> placedOrders, ICollection<ResolvedOrder> resolvedOrders)
		{
			PlacedOrders = placedOrders;
			ResolvedOrders = resolvedOrders;
		}
	}
}
