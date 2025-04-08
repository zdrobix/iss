namespace PharmacyApi.Models.Domain
{
	public class Order : Entity<int>
	{
        public ICollection<OrderedDrug> OrderedDrugs { get; set; }
        public DateTime DateTime{ get; set; }

        public Order(ICollection<OrderedDrug> orderedDrugs, DateTime dateTime)
		{
			OrderedDrugs = orderedDrugs;
			DateTime = dateTime;
		}

		public Order()
		{
			OrderedDrugs = new List<OrderedDrug>();
		}
	}
}
