namespace PharmacyApi.Models.Domain
{
	public class Hospital : OrderEntityContainer
	{
		public string Name { get; set; }
		public ICollection<HospitalStaff> Staff { get; set; }

		public Hospital(string name, ICollection<HospitalStaff> staff, ICollection<PlacedOrder> placedOrders, ICollection<ResolvedOrder> resolvedOrders)
			: base(placedOrders, resolvedOrders)
		{
			Name = name;
			Staff = staff;
		}

		public Hospital() : base(new List<PlacedOrder>(), new List<ResolvedOrder>())
		{
			Name = "";
			Staff = new List<HospitalStaff>();
		}
	}
}
