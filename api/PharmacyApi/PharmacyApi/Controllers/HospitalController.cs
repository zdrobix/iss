using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
							Role = staff.Role
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
					Name = hospital.Name,
					Staff = hospital.Staff.Select(staff => new UserDTO
					{
						Name = staff.Name,
						Username = staff.Username,
						Role = staff.Role
					}).ToList()
				}));
		}
	}
}
