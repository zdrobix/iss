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
	public class OrderController : ControllerBase
	{
		private readonly IUserRepository userRepository;
		private readonly IDrugRepository drugRepository;
		private readonly IOrderRepository orderRepository;

		public OrderController(IOrderRepository orderRepository, IUserRepository userRepository, IDrugRepository drugRepository)
		{
			this.orderRepository = orderRepository;
			this.userRepository = userRepository;
			this.drugRepository = drugRepository;
		}

		// POST : https://localhost:7282/api/order
		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] AddOrderRequestDTO request)
		{
			Log.Information("CreateOrder method called.");
			if (request == null)
			{
				Log.Error("Received null request.");
				return BadRequest();
			}
			Log.Information($"Creating order by {request.PlacedBy.Name}");

			var orderedDrugs = new List<OrderedDrug>();

			foreach (var d in request.OrderedDrugs)
			{
				var drug = await drugRepository.GetById(d.Drug.Id);
				orderedDrugs.Add(new OrderedDrug
				{
					Drug = drug,
					Quantity = d.Quantity
				});
			}
			var order = new Order
			{
				PlacedBy = await userRepository.GetById(request.PlacedBy.Id),
				OrderedDrugs = orderedDrugs,
				DateTime = request.DateTime
			};

			Log.Information("Adding order.");
			order = await this.orderRepository.CreateAsync(order);
			return Ok(
				DomainModelToDTO(order!)
			);
		}

		// GET : https://localhost:7282/api/order
		[HttpGet]
		public async Task<IActionResult> GetAllOrders() =>
			await this.orderRepository.GetAllAsync() is IEnumerable<Order> orders
				? Ok(orders.Select(order =>
					DomainModelToDTO(order!)
				).ToList()) : NotFound();

		// GET : https://localhost:7282/api/order
		[HttpGet("unresolved")]
		public async Task<IActionResult> GetAllUnresolvedOrders() =>
			await this.orderRepository.GetAllAsync() is IEnumerable<Order> orders
				? Ok(orders
					.Where(o => o.ResolvedBy == null)
					.Select(order =>
					DomainModelToDTO(order!)
				).ToList()) : NotFound();

		// GET : https://localhost:7282/api/order
		[HttpGet("resolved")]
		public async Task<IActionResult> GetAllResolvedOrders() =>
			await this.orderRepository.GetAllAsync() is IEnumerable<Order> orders
				? Ok(orders
					.Where(o => o.ResolvedBy != null)
					.Select(order =>
					DomainModelToDTO(order!)
				).ToList()) : NotFound();

		// GET : https://localhost:7282/api/order{id}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetOrderById([FromRoute] int id)
		{
			var order = await this.orderRepository.GetById(id);
			if (order == null)
			{
				Log.Information($"Couldn't find the order with id {id}");
				return NotFound();
			}
			return Ok(
				DomainModelToDTO(order!)
			);
		}

		// PUT : https://localhost:7383/api/order/{id}
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderRequestDTO request)
		{
			var order = await this.orderRepository.GetById(id);
			if (order == null)
			{
				return NotFound();
			}
			var existing = await this.userRepository.GetById(request.ResolvedById);
			if (existing == null)
			{
				return BadRequest("User with id null.");
			}
			order.ResolvedBy = existing;
			order = await this.orderRepository.UpdateAsync(order);
			return Ok(
				DomainModelToDTO(order!)	
			);
		}

		public static OrderDTO DomainModelToDTO(Order order) =>
			new OrderDTO
			{
				Id = order.Id,
				PlacedBy = new UserDTO
				{
					Id = order.PlacedBy.Id,
					Name = order.PlacedBy.Name,
					Username = order.PlacedBy.Username,
					Role = order.PlacedBy.Role,
					Password = "*****",
					Hospital = order.PlacedBy.Hospital == null ? null : new HospitalDTO 
					{ 
						Id = order.PlacedBy.Hospital.Id,
						Name = order.PlacedBy.Hospital.Name
					},
					Pharmacy = null
				},
				ResolvedBy =
					order.ResolvedBy == null ?
					null :
					new UserDTO
					{
						Id = order.ResolvedBy.Id,
						Name = order.ResolvedBy.Name,
						Username = order.ResolvedBy.Username,
						Role = order.ResolvedBy.Role,
						Password = "*****",
						Hospital = null,
						Pharmacy = order.ResolvedBy.Pharmacy == null ? null : new PharmacyDTO
						{
							Id = order.ResolvedBy.Pharmacy.Id,
							Name = order.ResolvedBy.Pharmacy.Name,
							Storage = new DrugStorageDTO()
						}
					},
				OrderedDrugs = order.OrderedDrugs.Select(
							d => new OrderedDrugDTO
							{
								Drug = new DrugDTO
								{
									Id = d.Drug.Id,
									Name = d.Drug.Name,
									Price = d.Drug.Price,
								},
								Quantity = d.Quantity
							}
				).ToList(),
				DateTime = order.DateTime
			};
	}
}
