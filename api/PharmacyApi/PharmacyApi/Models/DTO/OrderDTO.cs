namespace PharmacyApi.Models.DTO
{
	public class OrderDTO
	{
        public ICollection<OrderedDrugDTO> OrderedDrugs { get; set; }
        public DateTime DateTime{ get; set; }
	}
}
