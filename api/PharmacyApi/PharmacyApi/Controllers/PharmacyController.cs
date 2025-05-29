using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;
using Serilog;

namespace PharmacyApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class PharmacyController : ControllerBase
	{
		private readonly IPharmacyRepository pharmacyRepository;

		public PharmacyController(IPharmacyRepository pharmacyRepository)
		{
			this.pharmacyRepository = pharmacyRepository;
			Log.Information("PharmacyController initialized");
		}

		// POST: https://localhost:7282/api/pharmacy
		[HttpPost]
		public async Task<IActionResult> CreatePharmacy([FromBody] AddPharmacyRequestDTO request)
		{
			var pharmacy = new Pharmacy(
				request.Name, 
				new DrugStorage(),
				new OrderEntityContainer()
			);
			pharmacy = await pharmacyRepository.CreateAsync(pharmacy);
			return Ok(
				new PharmacyDTO
				{
					Id = pharmacy.Id,
					Name = pharmacy.Name,
					Storage = new DrugStorageDTO
					{
						Id = pharmacy.Storage.Id,
						StoredDrugs = pharmacy.Storage.StoredDrugs
									.Select(
										storedDrug => new StoredDrugDTO
										{
											Quantity = storedDrug.Quantity,
											Drug = new DrugDTO
											{
												Id = storedDrug.Drug.Id,
												Name = storedDrug.Drug.Name,
												Price = storedDrug.Drug.Price
											}
										}
									).ToList()
					}
				}
			);
		}

		// GET: http://localhost:7282/api/pharmacy
		[HttpGet]
		public async Task<IActionResult> GetAllPharmacies()
		{
			var pharmacies = await pharmacyRepository.GetAllAsync();
			if (pharmacies == null)
				return NotFound();
			return Ok(
				pharmacies.Select(
					pharmacy => new PharmacyDTO
						{
							Id = pharmacy.Id,
							Name = pharmacy.Name,
							Storage = new DrugStorageDTO
								{
									Id = pharmacy.Storage.Id,	
									StoredDrugs = pharmacy.Storage.StoredDrugs
									.Select(
										storedDrug => new StoredDrugDTO
										{
											Quantity = storedDrug.Quantity,
											Drug = new DrugDTO
											{
												Id = storedDrug.Drug.Id,
												Name = storedDrug.Drug.Name,
												Price = storedDrug.Drug.Price
											}
										}
									).ToList()
								}

					}
				)
			);
		}

		// GET: http://localhost:7282/api/pharmacy{id}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetPharmacyById(int id)
		{
			var pharmacy = await pharmacyRepository.GetById(id);
			if (pharmacy == null)
				return NotFound();
			return Ok(
				new PharmacyDTO
				{
					Id = pharmacy.Id,
					Name = pharmacy.Name,
					Storage = new DrugStorageDTO
					{
						Id = pharmacy.Storage.Id,
						StoredDrugs = pharmacy.Storage.StoredDrugs
									.Select(
										storedDrug => new StoredDrugDTO
										{
											Quantity = storedDrug.Quantity,
											Drug = new DrugDTO
											{
												Id = storedDrug.Drug.Id,
												Name = storedDrug.Drug.Name,
												Price = storedDrug.Drug.Price
											}
										}
									).ToList()
					}

				}
			);
		}

		// PUT: http://localhost:7282/api/pharmacy{id}
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdatePharmacy([FromRoute] int id, [FromBody] UpdatePharmacyRequestDTO request)
		{
			Log.Information($"Trying to update pharmacy with id {id}");

			var existingPharmacy = await this.pharmacyRepository.GetById(id);
			if (existingPharmacy == null)
			{
				Log.Information($"Couldn't find pharmacy with id {id}");
				return NotFound();
			}

			existingPharmacy.Name = request.Name;

			if (request.Storage != null && request.Storage.StoredDrugs != null)
			{
				existingPharmacy.Storage = (DrugStorage) new DrugStorage
				{
					StoredDrugs = request.Storage.StoredDrugs.Select(storedDrug => new StoredDrug
					{
						Quantity = storedDrug.Quantity,
						Drug = new Drug
						{
							Name = storedDrug.Drug.Name,
							Price = storedDrug.Drug.Price
						}
					}).ToList()
				}.SetId(existingPharmacy.Storage.Id);
			} else Log.Information("No storage to update");

			Log.Information("Updating pharmacy with id {id}", id);
			var updatedPharmacy = await pharmacyRepository.UpdateAsync(existingPharmacy);

			if (updatedPharmacy == null)
			{
				Log.Information($"Couldn't update pharmacy with id {id}");
				return NotFound();
			}
			return Ok(new PharmacyDTO
					{
					Id = updatedPharmacy.Id,
					Name = updatedPharmacy.Name,
					Storage = new DrugStorageDTO
					{
						StoredDrugs = updatedPharmacy.Storage.StoredDrugs
									.Select(
										storedDrug => new StoredDrugDTO
										{
											Quantity = storedDrug.Quantity,
											Drug = new DrugDTO
											{
												Id = storedDrug.Drug.Id,
												Name = storedDrug.Drug.Name,
												Price = storedDrug.Drug.Price
											}
										}
									).ToList()
					}
				});
		}
	}
}
