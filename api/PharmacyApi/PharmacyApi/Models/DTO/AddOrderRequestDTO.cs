namespace PharmacyApi.Models.DTO
{
    public class AddOrderRequestDTO
    {
        public UserDTO PlacedBy { get; set; }
        public ICollection<OrderedDrugDTO> OrderedDrugs { get; set; }
        public DateTime DateTime { get; set; }
    }
}
