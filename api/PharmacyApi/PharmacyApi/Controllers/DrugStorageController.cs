using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Implementation;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class DrugStorageController : ControllerBase
	{
		private readonly IDrugStorageRepository drugStorageRepository;
		private readonly IDrugRepository drugRepository;
		private readonly IStoredDrugRepository storedDrugRepository;

		public DrugStorageController(IDrugStorageRepository drugStorageRepository, IDrugRepository drugRepository, IStoredDrugRepository storedDrugRepository)
		{
			this.drugStorageRepository = drugStorageRepository;
			this.drugRepository = drugRepository;
			this.storedDrugRepository = storedDrugRepository;
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
					Id = drugStorage.Id,
					StoredDrugs = drugStorage.StoredDrugs
						.Select(
							storedDrug => new StoredDrugDTO
							{
								Id = storedDrug.Id,
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
						Id = drugStorage.Id,
						StoredDrugs = drugStorage.StoredDrugs
						.Select(
							storedDrug => new StoredDrugDTO
							{
								Id = storedDrug.Id,
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

		// PUT : http://localhost:7282/api/drugstorage{id}
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateDrugStorage([FromRoute] int id, [FromBody] UpdateDrugStorageRequestDTO request, IDrugStorageRepository drugStorageRepository)
		{
			var drugStorage = await drugStorageRepository.GetById(id);
			if (drugStorage == null)
			{
				return NotFound();
			}

			foreach (var storedDrug in request.StoredDrugs)
			{
				var existing = await this.storedDrugRepository.GetByDrugIdAndStorageId(storedDrug.Drug.Id, id);
				if (existing == null)
				{
					var newStoredDrug = new StoredDrug
					{
						Drug = await this.drugRepository.GetById(storedDrug.Drug.Id),
						Quantity = storedDrug.Quantity,
						Storage = await this.drugStorageRepository.GetById(id)
					};

					await this.storedDrugRepository.CreateAsync(newStoredDrug);
				} else
				{
					existing.Quantity = storedDrug.Quantity;
					await this.storedDrugRepository.UpdateAsync(existing);
				}
			}

			drugStorage = await drugStorageRepository.GetById(id);

			return drugStorage == null ? BadRequest() : Ok(
			new DrugStorageDTO
			{
				Id = drugStorage.Id,
				StoredDrugs = drugStorage.StoredDrugs
					.Select(
						storedDrug => new StoredDrugDTO
						{	
							Id = storedDrug.Id,
							Quantity = storedDrug.Quantity,
							Drug = new DrugDTO
							{
								Name = storedDrug.Drug.Name,
								Price = storedDrug.Drug.Price
							}
						}
					).ToList()
			});
		}
	}
}
