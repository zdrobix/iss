using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PharmacyApi.Models.DTO
{
	public class StoredDrugDTO
	{
		public int Quantity { get; set; }
		public DrugDTO Drug { get; set; }
	}
}
