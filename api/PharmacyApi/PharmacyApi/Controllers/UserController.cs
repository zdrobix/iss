using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Implementation;
using PharmacyApi.Repo.Interface;
using PharmacyApi.Utils;
using Serilog;


namespace PharmacyApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository userRepository;
		private readonly IHospitalRepository hospitalRepository;
		private readonly IPharmacyRepository pharmacyRepository;
		private readonly ITokenService tokenService;

		public UserController(IUserRepository userRepository, IHospitalRepository hospitalRepository,
						  IPharmacyRepository pharmacyRepository, ITokenService tokenService)
		{
			this.userRepository = userRepository;
			this.hospitalRepository = hospitalRepository;
			this.pharmacyRepository = pharmacyRepository;
			this.tokenService = tokenService;
			Log.Information("UserController initialized");
		}

		// POST : https://localhost:7282/api/user
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
		{
			var user = new User
			(
				userDTO.Name,
				userDTO.Username,
				PasswordHasher.Encrypt(userDTO.Password),
				userDTO.Role
			);

			user = await userRepository.CreateAsync(user);
			return Ok(
				new UserDTO
				{
					Id = user.Id,
					Name = user.Name,
					Username = user.Username,
					Password = user.Password,
					Role = user.Role
				}
			);
		}

		// GET : https://localhost:7282/api/user
		[Authorize]
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			Log.Information($"Searching for a user with id {id}");
			var user = await userRepository.GetById(id);
			if (user == null)
			{
				Log.Information($"Couldn't find user with id {id}");
				return NotFound();
			}
			return Ok(
				new UserDTO
				{
					Id = user.Id,
					Name = user.Name,
					Username = user.Username,
					Password = user.Password,
					Role = user.Role,
					Hospital = user.Hospital == null ? null : new HospitalDTO
					{
						Id = user.Hospital.Id,
						Name = user.Hospital.Name,
					},
					Pharmacy = user.Pharmacy == null ? null : new PharmacyDTO
					{
						Id = user.Pharmacy.Id,
						Name = user.Pharmacy.Name,
						Storage = new DrugStorageDTO()
					}
				}
			);
		}

		// POST : https://localhost:7282/api/user
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
		{
			Log.Information($"{request.Username} tries to login with password {request.Password}");
			var user = await userRepository.GetByUsername(request.Username);
			if (user == null)
			{
				Log.Information($"User {request.Username} not found.");
				return NotFound();
			}
			if (PasswordHasher.Encrypt(request.Password) != user.Password)
			{
				Log.Information($"User {request.Username} failed to login by providing an incorrect password.");
				return Unauthorized();
			}

			Log.Information($"User {request.Username} logged in successfully.");

			var token = tokenService.GenerateToken(user);
			var userDto = new UserDTO
			{
				Id = user.Id,
				Name = user.Name,
				Username = user.Username,
				Password = user.Password,
				Role = user.Role,
				Pharmacy = user.Pharmacy == null ? null : new PharmacyDTO
				{
					Id = user.Pharmacy.Id,
					Name = user.Pharmacy.Name,
					Storage = new DrugStorageDTO
					{
						Id = user.Pharmacy.Storage.Id,
						StoredDrugs = user.Pharmacy.Storage.StoredDrugs
							.Select(storedDrug => new StoredDrugDTO
							{
								Id = storedDrug.Id,
								Quantity = storedDrug.Quantity,
								Drug = new DrugDTO
								{
									Id = storedDrug.Drug.Id,
									Name = storedDrug.Drug.Name,
									Price = storedDrug.Drug.Price
								},
								StorageId = user.Pharmacy.Storage.Id
							}).ToList()
					}
				},
				Hospital = user.Hospital == null ? null : new HospitalDTO
				{
					Id = user.Hospital.Id,
					Name = user.Hospital.Name
				}
			};
			return Ok(new { token, user });
		}

		// GET : https://localhost:7282/api/user
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			Log.Information("Fetching users");
			return await userRepository.GetAllAsync() is IEnumerable<User> users
				? Ok(users.Select(user => new UserDTO
				{
					Id = user.Id,
					Name = user.Name,
					Username = user.Username,
					Password = "******",
					Role = user.Role,
					Pharmacy = user.Pharmacy == null ? null : new PharmacyDTO
					{
						Id = user.Pharmacy.Id,
						Name = user.Pharmacy.Name,
						Storage = new DrugStorageDTO()
					},
					Hospital = user.Hospital == null ? null : new HospitalDTO
					{
						Id = user.Hospital.Id,
						Name = user.Hospital.Name
					}
				}))
				: NotFound();
		}

		// DELETE : https://localhost:7282/api/user{id}
		[Authorize]
		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> DeleteUser([FromRoute] int id)
		{
			var user = await userRepository.DeleteAsync(id);

			return user == null ? NotFound() : Ok(
				new UserDTO
				{
					Id = user.Id,
					Name = user.Name,
					Username = user.Username,
					Password = "******",
					Role = user.Role,
					Pharmacy = user.Pharmacy == null ? null : new PharmacyDTO
					{
						Id = user.Pharmacy.Id,
						Name = user.Pharmacy.Name,
						Storage = new DrugStorageDTO()
					},
					Hospital = user.Hospital == null ? null : new HospitalDTO
					{
						Id = user.Hospital.Id,
						Name = user.Hospital.Name
					}
				}
			);
		}

		// PUT : https://localhost:7282/api/user{id}
		[Authorize]
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequestDTO request)
		{
			Log.Information($"Trying to update user with id {id}");
			var user = await userRepository.GetById(id);
			if (user == null)
			{
				Log.Information($"Couldn't find user with id {id}");
				return NotFound();
			}

			user.Name = request.Name;
			user.Username = request.Username;
			user.Password = PasswordHasher.Encrypt(request.Password);
			user.Role = request.Role;

			if (request.Hospital != null)
			{
				var hospital = await hospitalRepository.GetById(request.Hospital.Id);
				if (hospital == null)
				{
					return BadRequest("Invalid hospital ID.");
				}
				user.Hospital = hospital;
			}

			if (request.Pharmacy != null)
			{
				var pharmacy = await pharmacyRepository.GetById(request.Pharmacy.Id);
				if (pharmacy == null)
				{
					return BadRequest("Invalid pharmacy ID.");
				}
				user.Pharmacy = pharmacy;
			}

			Log.Information($"Updating user with id {id}");
			user = await userRepository.UpdateAsync(user);

			if (user == null)
			{
				Log.Information($"Couldn't update user with id {id}");
				return NotFound();
			}

			return Ok(
				new UserDTO
				{
					Id = user.Id,
					Name = user.Name,
					Username = user.Username,
					Password = user.Password,
					Role = user.Role,
					Pharmacy = user.Pharmacy == null ? null : new PharmacyDTO
					{
						Id = user.Pharmacy.Id,
						Name = user.Pharmacy.Name,
						Storage = new DrugStorageDTO()
					},
					Hospital = user.Hospital == null ? null : new HospitalDTO
					{
						Id = user.Hospital.Id,
						Name = user.Hospital.Name
					}
				}
			);
		}
	}
}
