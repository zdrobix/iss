namespace PharmacyApi.Models.Domain
{
	public class Drug : Entity<int>
	{
        public string Name { get; set; }
        public float Price { get; set; }

        public Drug (string name, float price)
		{
			this.Name = name;
			this.Price = price;
		}
	}
}
