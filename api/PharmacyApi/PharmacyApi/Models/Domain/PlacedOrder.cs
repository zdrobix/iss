namespace PharmacyApi.Models.Domain
{
	public class PlacedOrder : Order
	{
		public User PlacedBy { get; set; }

		public PlacedOrder(ICollection<OrderedDrug> orderedDrugs, DateTime dateTime, User placedBy) : base(orderedDrugs, dateTime)
		{
			this.PlacedBy = placedBy;
		}

		public PlacedOrder() : base(new List<OrderedDrug>(), DateTime.Now)
		{}
	}
}
