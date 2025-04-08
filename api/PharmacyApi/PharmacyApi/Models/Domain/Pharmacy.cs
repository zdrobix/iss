namespace PharmacyApi.Models.Domain
{
	public class Pharmacy : OrderEntityContainer
	{
		public string Name { get; set; }
		public ICollection<User> Staff { get; set; }
		public DrugStorage Storage { get; set; }

		public Pharmacy(string name, ICollection<User> staff, DrugStorage storage, ICollection<PlacedOrder> placedOrders, ICollection<ResolvedOrder> resolvedOrders)
			: base(placedOrders, resolvedOrders)
		{
			Name = name;
			Staff = staff;
			Storage = storage;
		}

		public Pharmacy() : base( new List<PlacedOrder>(), new List<ResolvedOrder>())
		{
			Name = "";
			Staff = new List<User>();
			Storage = new DrugStorage();
		}
	}
}
