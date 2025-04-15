using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Interface;
using PharmacyApi.Utils;


namespace PharmacyApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository userRepository;

		public UserController(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		// POST : https://localhost:7282/api/user
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
					Name = user.Name,
					Username = user.Username,
					Password = user.Password,
					Role = user.Role
				}
			);
		}

		// GET : https://localhost:7282/api/user
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await userRepository.GetById(id);
			if (user == null)
				return NotFound();
			return Ok(
				new UserDTO
				{
					Name = user.Name,
					Username = user.Username,
					Password = user.Password,
					Role = user.Role
				}
			);
		}

		// GET : https://localhost:7282/api/user
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
		{
			var user = await userRepository.GetByUsername(request.Username);
			if (user == null)
				return NotFound();
			if (PasswordHasher.Encrypt(request.Password) != user.Password)
				return Unauthorized();

			return Ok(new UserDTO
			{
				Name = user.Name,
				Username = user.Username,
				Password = user.Password,
				Role = user.Role
			});
		}

	}
}
