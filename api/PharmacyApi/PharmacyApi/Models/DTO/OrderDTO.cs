namespace PharmacyApi.Models.DTO
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public UserDTO PlacedBy { get; set; }
		public UserDTO? ResolvedBy {  get; set; }
        public ICollection<OrderedDrugDTO> OrderedDrugs { get; set; }
        public DateTime DateTime{ get; set; }
	}
}
