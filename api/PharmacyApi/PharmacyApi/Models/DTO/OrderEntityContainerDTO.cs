namespace PharmacyApi.Models.DTO
{
	public class OrderEntityContainerDTO 
	{
		public ICollection<PlacedOrderDTO> PlacedOrders { get; set; }
		public ICollection<ResolvedOrderDTO> ResolvedOrders { get; set; }
	}
}
