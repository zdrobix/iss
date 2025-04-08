using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DrugStorageController : ControllerBase
	{
		private readonly IDrugStorageRepository drugStorageRepository;

        public DrugStorageController(IDrugStorageRepository drugStorageRepository)
        {
			this.drugStorageRepository = drugStorageRepository;
		}

		// GET: http://localhost:7282/api/drugstorage{id}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetDrugStorageById(int id)
		{
			var drugStorage = await drugStorageRepository.GetById(id);
			if (drugStorage == null)
				return NotFound();
			return Ok(
				new DrugStorageDTO
				{
					StoredDrugs = drugStorage.StoredDrugs
						.Select(
							storedDrug => new StoredDrugDTO
								{
									Quantity = storedDrug.Quantity,
									Drug = new DrugDTO
										{
											Name = storedDrug.Drug.Name,
											Price = storedDrug.Drug.Price
										}
								}
						).ToList()
				}	
			);
		}

		// GET: http://localhost:7282/api/drugstorage
		[HttpGet]
		public async Task<IActionResult> GetAllDrugStorages()
		{
			var drugStorages = await drugStorageRepository.GetAllAsync();
			if (drugStorages == null)
				return NotFound();
			return Ok(
				drugStorages.Select(
					drugStorage => new DrugStorageDTO
					{
						StoredDrugs = drugStorage.StoredDrugs
						.Select(
							storedDrug => new StoredDrugDTO
							{
								Quantity = storedDrug.Quantity,
								Drug = new DrugDTO
								{
									Name = storedDrug.Drug.Name,
									Price = storedDrug.Drug.Price
								}
							}
						).ToList()
					}
				)
			);
		}

	}
}
