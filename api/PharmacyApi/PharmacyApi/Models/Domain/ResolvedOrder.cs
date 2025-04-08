namespace PharmacyApi.Models.Domain
{
	public class ResolvedOrder : Order
	{
        public User ResolvedBy { get; set; }

		public ResolvedOrder(ICollection<OrderedDrug> orderedDrugs, DateTime dateTime, User resolvedBy) : base(orderedDrugs, dateTime)
		{
			this.ResolvedBy = resolvedBy;
		}

		public ResolvedOrder() : base(new List<OrderedDrug>(), DateTime.Now) { }

	}
}
