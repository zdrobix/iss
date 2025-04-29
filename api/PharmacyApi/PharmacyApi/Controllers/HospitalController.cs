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
	public class HospitalController : ControllerBase
	{
		private readonly IHospitalRepository hospitalRepository;
		private readonly IUserRepository userRepository;

		public HospitalController(IHospitalRepository hospitalRepository, IUserRepository userRepository)
        {
			this.hospitalRepository = hospitalRepository;
			this.userRepository = userRepository;
		}

		// POST: https://localhost:7282/api/hospital
		[HttpPost]
		public async Task<IActionResult> CreateHospital([FromBody] AddHospitalRequestDTO request)
		{
			var hospital = new Hospital(
				request.Name,
				new OrderEntityContainer()
			);
			hospital = await hospitalRepository.CreateAsync(hospital);
			return Ok(
				new HospitalDTO
				{
					Id = hospital.Id,
					Name = hospital.Name
				}
			);
		}

		// GET: https://localhost:7282/api/hospital{id}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetHospitalById(int id)
		{
			Log.Information($"Searching for a hospital with id {id}");
			var hospital = await hospitalRepository.GetById(id);
			if (hospital == null)
			{
				Log.Information($"Couldn't find hospital with id {id}");
				return NotFound();
			}
			return Ok(
				new HospitalDTO
				{
					Name = hospital.Name
				}
			);
		}

		// GET: https://localhost:7282/api/hospital 
		[HttpGet]
		public async Task<IActionResult> GetAllHospitals()
		{
			Log.Information("Getting all hospitals");
			var hospitals = await hospitalRepository.GetAllAsync();
			if (hospitals == null)
				return NotFound();
			return Ok(
				hospitals.Select(hospital => new HospitalDTO
				{
					Id = hospital.Id,
					Name = hospital.Name,
				}));
		}

		// PUT : https://localhost:7282/api/hospital{id}
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateHospital([FromRoute] int id, [FromBody] UpdateHospitalRequestDTO request)
		{
			Log.Information($"Trying to update hospital with id {id}");
			var existingHospital = await this.hospitalRepository.GetById(id);

			if (existingHospital == null)
			{
				Log.Information($"Couldn't find hospital with id {id}");
				return NotFound();
			}

			existingHospital.Name = request.Name;
			Log.Information($"Updating hospital with id {id}");
			existingHospital = await hospitalRepository.UpdateAsync(existingHospital);
			if (existingHospital == null)
			{
				Log.Information($"Couldn't update hospital with id {id}");
				return NotFound();
			}
			return Ok(
				new HospitalDTO
				{
					Id = existingHospital.Id,
					Name = existingHospital.Name,
				}
			);
		}
	}
}
