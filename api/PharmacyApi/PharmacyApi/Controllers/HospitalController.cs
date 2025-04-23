using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HospitalController : ControllerBase
	{
		private readonly IHospitalRepository hospitalRepository;

        public HospitalController(IHospitalRepository hospitalRepository)
        {
			this.hospitalRepository = hospitalRepository;
		}

		// POST: https://localhost:7282/api/hospital
		[HttpPost]
		public async Task<IActionResult> CreateHospital([FromBody] AddHospitalRequestDTO request)
		{
			var hospital = new Hospital(
				request.Name,
				new List<User>(),
				new List<PlacedOrder>(),
				new List<ResolvedOrder>()
			);
			hospital = await hospitalRepository.CreateAsync(hospital);
			return Ok(
				new HospitalDTO
				{
					Id = hospital.Id,
					Name = hospital.Name,
					Staff = hospital.Staff.Select(staff => new UserDTO
					{
						Name = staff.Name,
						Username = staff.Username,
						Role = staff.Role,
						Password = staff.Password,
						HospitalId = staff.HospitalId
					}).ToList()
				}

			);
		}

		// GET: https://localhost:7282/api/hospital{id}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetHospitalById(int id)
		{
			var hospital = await hospitalRepository.GetById(id);
			if (hospital == null)
				return NotFound();
			return Ok(
				new HospitalDTO
				{
					Name = hospital.Name,
					Staff = hospital.Staff.Select(staff => new UserDTO
					{
							Name = staff.Name,
							Username = staff.Username,
							Role = staff.Role,
							Password = staff.Password,
							HospitalId = staff.HospitalId
					}).ToList()
				}
			);
		}

		// GET: https://localhost:7282/api/hospital 
		[HttpGet]
		public async Task<IActionResult> GetAllHospitals()
		{
			var hospitals = await hospitalRepository.GetAllAsync();
			if (hospitals == null)
				return NotFound();
			return Ok(
				hospitals.Select(hospital => new HospitalDTO
				{
					Id = hospital.Id,
					Name = hospital.Name,
					Staff = hospital.Staff.Select(staff => new UserDTO
					{
						Name = staff.Name,
						Username = staff.Username,
						Password = staff.Password,
						Role = staff.Role,
						HospitalId = staff.HospitalId
					}).ToList()
				}));
		}
	}
}
