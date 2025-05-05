using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Implementation;
using PharmacyApi.Repo.Interface;
using Serilog;

namespace PharmacyApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoredDrugController : ControllerBase
	{
		private readonly IDrugStorageRepository drugStorageRepository;
		private readonly IDrugRepository drugRepository;
		private readonly IStoredDrugRepository storedDrugRepository;

		public StoredDrugController(IDrugStorageRepository drugStorageRepository, IDrugRepository drugRepository, IStoredDrugRepository storedDrugRepository)
		{
			this.drugStorageRepository = drugStorageRepository;
			this.drugRepository = drugRepository;
			this.storedDrugRepository = storedDrugRepository;
		}

		// POST : http://localhost:7282/api/storeddrug
		[HttpPost]
		public async Task<IActionResult> CreateOrUpdateStoredDrug([FromBody] AddStoredDrugRequestDTO request)
		{
			Log.Information($"StoredDrug method called.");
			var existing = await this.storedDrugRepository.GetByDrugIdAndStorageId(request.Drug.Id, request.StorageId);
			var storedDrug = new StoredDrug();		

			var drug = await this.drugRepository.GetById(request.Drug.Id);
			var storage = await this.drugStorageRepository.GetById(request.StorageId);

			if (drug == null || storage == null)
				return NotFound();

			if (existing == null)
			{
				Log.Information($"Didn't find drug {request.Drug.Name} at drug storage {request.StorageId}.");
				storedDrug = new StoredDrug
				{
					Drug = drug,
					Quantity = request.Quantity,
					Storage = storage
				};
				storedDrug = await this.storedDrugRepository.CreateAsync(storedDrug);
			} else
			{
				Log.Information($"Found drug {request.Drug.Name} at drug storage {request.StorageId}.");
				existing.Quantity = request.Quantity;
				storedDrug = await this.storedDrugRepository.UpdateAsync(existing);
			}
			return storedDrug == null ? NotFound() :
				Ok(new StoredDrugDTO
				{
					Id = storedDrug.Id,
					Drug = new DrugDTO
					{
						Id = storedDrug.Drug.Id,
						Name = storedDrug.Drug.Name,
						Price = storedDrug.Drug.Price,
					},
					Quantity = storedDrug.Quantity,
					StorageId = storedDrug.Storage.Id,
					Storage = new DrugStorageDTO
					{
						Id = storedDrug.Storage.Id,
						StoredDrugs = storedDrug.Storage.StoredDrugs
							.Select(storedDrug => new StoredDrugDTO
							{
								Quantity = storedDrug.Quantity,
								Drug = new DrugDTO
								{
									Name = storedDrug.Drug.Name,
									Price = storedDrug.Drug.Price
								}
							}).ToList()
					}
				});
		}

		// GET : http://localhost:7282/api/storeddrug
		[HttpGet("{drugId}/stored/{storageId}")]
		public async Task<IActionResult> GetStoredDrugByDrugIdAndStorageId([FromRoute] int drugId, [FromRoute] int storageId)
		{
			Log.Information($"Getting drug with id {drugId} from storage with id {storageId}.");
			var existing = await this.storedDrugRepository.GetByDrugIdAndStorageId(drugId, storageId);
			if (existing == null)
			{
				var drug = await this.drugRepository.GetById(drugId);
				if (drug == null)
				{
					Log.Information($"Couldn't find drug with id {drugId}");
					return NotFound();
				}
				Log.Information($"Couldn't find stored drug with drug id {drugId} and storage id {storageId}");
				return Ok(
					new StoredDrugDTO
					{
						Quantity = 0,
						Drug = new DrugDTO
						{
							Id = drug.Id,
							Name = drug.Name,
							Price = drug.Price
						},
						StorageId = storageId,
						Storage = new DrugStorageDTO
						{
							Id = storageId,
							StoredDrugs = new List<StoredDrugDTO>()
						}
					}	
				);
			}
			Log.Information($"Found drug with id {drugId} in storage with id {storageId}, q = {existing.Quantity}");
			return Ok(
				new StoredDrugDTO
				{
					Id = existing.Id,
					Quantity = existing.Quantity,
					Drug = new DrugDTO
					{
						Id = existing.Drug.Id,
						Name = existing.Drug.Name,
						Price = existing.Drug.Price
					},
					StorageId = storageId,
					Storage = new DrugStorageDTO
					{
						Id = storageId,
						StoredDrugs = new List<StoredDrugDTO>()
					}
				}
			);
		}
	}
}
