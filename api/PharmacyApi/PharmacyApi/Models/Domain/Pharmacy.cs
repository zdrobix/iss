namespace PharmacyApi.Models.Domain
{
	public class Pharmacy : OrderEntityContainer
	{
		public string Name { get; set; }
		public ICollection<PharmacyStaff> Staff { get; set; }
		public DrugStorage Storage { get; set; }

		public Pharmacy(string name, ICollection<PharmacyStaff> staff, DrugStorage storage, ICollection<PlacedOrder> placedOrders, ICollection<ResolvedOrder> resolvedOrders)
			: base(placedOrders, resolvedOrders)
		{
			Name = name;
			Staff = staff;
			Storage = storage;
		}

		public Pharmacy() : base( new List<PlacedOrder>(), new List<ResolvedOrder>())
		{
			Name = "";
			Staff = new List<PharmacyStaff>();
			Storage = new DrugStorage();
		}
	}
}
