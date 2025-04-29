namespace PharmacyApi.Models.Domain
{
	public class Order : Entity<int>
	{
		public User PlacedBy { get; set; }
		public User? ResolvedBy { get; set; }
        public ICollection<OrderedDrug> OrderedDrugs { get; set; }
        public DateTime DateTime{ get; set; }
	

		public Order(User placedBy, ICollection<OrderedDrug> orderedDrugs, DateTime dateTime)
		{
			PlacedBy = placedBy;
			OrderedDrugs = orderedDrugs;
			DateTime = dateTime;
		}

		public Order()
		{
			PlacedBy = new User();
			OrderedDrugs = new List<OrderedDrug>();
		}
	}
}
