namespace PharmacyApi.Models.Domain
{
	public class Hospital : Entity<int>
	{
		public string Name { get; set; }
		public OrderEntityContainer OrderContainer { get; set; }

		public Hospital(string name, OrderEntityContainer orderContainer)
		{
			Name = name;
			OrderContainer = orderContainer;
		}

		public Hospital()
		{
		}
	}
}
