namespace PharmacyApi.Models.Domain
{
	public class Pharmacy : Entity<int>
	{
		public string Name { get; set; }
		public DrugStorage Storage { get; set; }
		public OrderEntityContainer OrderContainer { get; set; }

		public Pharmacy(string name, DrugStorage storage, OrderEntityContainer orderContainer)
		{
			Name = name;
			Storage = storage;
			OrderContainer = orderContainer;
		}

		public Pharmacy()
		{
			Name = "";
			Storage = new DrugStorage();
			OrderContainer = new OrderEntityContainer();
		}
	}
}
