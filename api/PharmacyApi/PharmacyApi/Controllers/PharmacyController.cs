using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;
using Serilog;

namespace PharmacyApi.Controllers
{
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
				new List<User>(),
				new DrugStorage { StoredDrugs = new List<StoredDrug>() },
				new List<PlacedOrder>(),
				new List<ResolvedOrder>()
			);
			pharmacy = await pharmacyRepository.CreateAsync(pharmacy);
			return Ok(
				new PharmacyDTO
				{
					Id = pharmacy.Id,
					Name = pharmacy.Name,
					Staff = pharmacy.Staff.Select(s => new UserDTO
					{
						Name = s.Name,
						Username = s.Username,
						Role = s.Role
					}).ToList(),
					Storage = new DrugStorageDTO
					{
						StoredDrugs = pharmacy.Storage.StoredDrugs
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
							Staff = pharmacy.Staff.Select(s => new UserDTO
							{
								Name = s.Name,
								Username = s.Username,
								Role = s.Role,
								Password = s.Password,
								PharmacyId = pharmacy.Id,
							}).ToList(),
							Storage = new DrugStorageDTO
								{
									StoredDrugs = pharmacy.Storage.StoredDrugs
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
					Staff = pharmacy.Staff.Select(s => new UserDTO
					{
						Name = s.Name,
						Username = s.Username,
						Role = s.Role,
						Password = s.Password,
						PharmacyId = pharmacy.Id,
					}).ToList(),
					Storage = new DrugStorageDTO
					{
						StoredDrugs = pharmacy.Storage.StoredDrugs
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

				}
			);
		}

		// PUT: http://localhost:7282/api/pharmacy{id}
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdatePharmacy([FromRoute] int id, [FromBody] UpdatePharmacyRequestDTO request)
		{
			Log.Information($"Searching for pharmacy with id {id}");

			var existingPharmacy = await this.pharmacyRepository.GetById(id);
			if (existingPharmacy == null)
			{
				Log.Information($"Couldn't find pharmacy with id {id}");
				return NotFound();
			}

			existingPharmacy.Name = request.Name;

			if (request.Staff != null)
			{
				existingPharmacy.Staff = request.Staff.Select(s => new User
				{
					Name = s.Name,
					Username = s.Username,
					Password = s.Password,
					Role = s.Role,
					PharmacyId = id
				}).ToList();
			} else Log.Information("No staff to update");

			if (request.Storage != null && request.Storage.StoredDrugs != null)
			{
				existingPharmacy.Storage = new DrugStorage
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
				};
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
					Staff = updatedPharmacy.Staff.Select(s => new UserDTO
					{
						Name = s.Name,
						Username = s.Username,
						Password = s.Password,
						Role = s.Role
					}).ToList(),
					Storage = new DrugStorageDTO
					{
						StoredDrugs = updatedPharmacy.Storage.StoredDrugs
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
				});
		}
	}
}
