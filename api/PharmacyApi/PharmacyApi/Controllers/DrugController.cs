using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;

namespace PharmacyApi.Controllers
{
	[Authorize]
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
					Id = drug.Id,
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
				Id = drug.Id,
				Name = drug.Name,
				Price = drug.Price
			});
			return Ok(drugDTOs);
		}

		//GET : https://localhost:7282/api/drugs{id}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetDrugById([FromRoute] int id)
		{
			var drug = await drugRepository.GetById(id);
			if (drug == null)
				return NotFound();

			return Ok(new DrugDTO
			{
				Id = drug.Id,
				Name = drug.Name,
				Price = drug.Price
			});
		}

		// PUT: https://localhost:7282/api/drug{id}
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateDrug([FromRoute] int id, [FromBody] UpdateDrugRequestDTO request)
		{
			var drug = (Drug)new Drug(request.Name, request.Price).SetId(id);

			drug = await drugRepository.UpdateAsync(drug);

			return drug == null ? 
				NotFound() : 
				Ok(new DrugDTO
				{
					Id = drug.Id,
					Name = drug.Name,
					Price = drug.Price
				});
		}

		// DELETE: https://localhost:7282/api/drug{id}
		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> DeleteDrug([FromRoute] int id)
		{
			var drug = await drugRepository.DeleteAsync(id);

			return drug == null ?
				NotFound() :
				Ok(new DrugDTO
				{
					Id = drug.Id,
					Name = drug.Name,
					Price = drug.Price
				});
		}
	}
}

