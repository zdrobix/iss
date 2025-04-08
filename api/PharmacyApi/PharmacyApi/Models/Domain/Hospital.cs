namespace PharmacyApi.Models.Domain
{
	public class Hospital : OrderEntityContainer
	{
		public string Name { get; set; }
		public ICollection<User> Staff { get; set; }

		public Hospital(string name, ICollection<User> staff, ICollection<PlacedOrder> placedOrders, ICollection<ResolvedOrder> resolvedOrders)
			: base(placedOrders, resolvedOrders)
		{
			Name = name;
			Staff = staff;
		}

		public Hospital() : base(new List<PlacedOrder>(), new List<ResolvedOrder>())
		{
			Name = "";
			Staff = new List<User>();
		}
	}
}
