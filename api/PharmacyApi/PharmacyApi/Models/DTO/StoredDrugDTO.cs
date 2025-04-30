using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PharmacyApi.Models.DTO
{
	public class StoredDrugDTO
{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public DrugDTO Drug { get; set; }
		public int StorageId { get; set; }
		public DrugStorageDTO Storage { get; set; }
	}
}
