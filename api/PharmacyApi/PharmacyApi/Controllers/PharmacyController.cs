using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;

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
		}

		// GET: http://localhost:7282/api/pharmacies
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
							Name = pharmacy.Name,
							Staff = pharmacy.Staff.Select(s => new UserDTO
							{
								Name = s.Name,
								Username = s.Username,
								Role = s.Role
							}).ToList()
						}
				)
			);
		}

		// GET: http://localhost:7282/api/pharmacies{id}
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
					Name = pharmacy.Name,
					Staff = pharmacy.Staff.Select(s => new UserDTO
					{
						Name = s.Name,
						Username = s.Username,
						Role = s.Role
					}).ToList()
				}
			);
		}
	}
}
