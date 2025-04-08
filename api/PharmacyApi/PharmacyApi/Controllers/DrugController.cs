using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DrugController : ControllerBase
	{
		private readonly IDrugRepository drugRepository;

		public DrugController(IDrugRepository drugRepository)
		{
			this.drugRepository = drugRepository;
		}

		// POST: https://localhost:7282/api/drug
		[HttpPost]
		public async Task<IActionResult> CreateDrug([FromBody] DrugDTO drugDTO)
		{
			var drug = new Drug(drugDTO.Name, drugDTO.Price);
			drug = await drugRepository.CreateAsync(drug);
			return Ok(
				new DrugDTO
				{
					Name = drug.Name,
					Price = drug.Price
				}
			);
		}

		//GET : https://localhost:7282/api/drugs
		[HttpGet]
		public async Task<IActionResult> GetAllDrugs()
		{
			var drugs = await drugRepository.GetAllAsync();
			var drugDTOs = drugs.Select(drug => new DrugDTO
			{
				Name = drug.Name,
				Price = drug.Price
			});
			return Ok(drugDTOs);
		}

		//GET : https://localhost:7282/api/drugs{id}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetDrugById([FromRoute]int id)
		{
			var drug = await drugRepository.GetById(id);
			if (drug == null)
				return NotFound();

			return Ok(new DrugDTO
				{
					Name = drug.Name,
					Price = drug.Price
				});
		}
	}
}
