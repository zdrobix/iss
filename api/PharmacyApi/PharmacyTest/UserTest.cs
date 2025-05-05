using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyApi.Controllers;
using PharmacyApi.Data;
using PharmacyApi.Models.Domain;
using PharmacyApi.Models.DTO;
using PharmacyApi.Repo.Implementation;
using PharmacyApi.Repo.Interface;
using PharmacyApi.Utils;

namespace PharmacyTest
{
	public class UserTest
	{

		[Test]
		public async Task CreateUser_ShouldReturnOk_WhenUserIsAdded()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var request = new UserDTO
			{
				Name = "John Doe",
				Username = "johndoe",
				Password = "password",
				Role = "ADMIN"
			};

			var createdUser = (User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1);
			mockUserRepo.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
				.ReturnsAsync(createdUser);

			var result = await controller.CreateUser(request);
			Assert.IsInstanceOf<OkObjectResult>(result);

			var userResult = result as OkObjectResult;
			Assert.IsNotNull(userResult);
			Assert.AreEqual(createdUser.Id, ((UserDTO)userResult!.Value).Id);
		}

		[Test]
		public async Task GetUserById_ShouldReturnOk_WhenIdIsValid()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var expectedUser = (User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1);

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			mockUserRepo.Setup(repo => repo.GetById(1))
				.ReturnsAsync((User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1));

			var result = await controller.GetUserById(1);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

		[Test]
		public async Task GetUserById_ShouldReturnNotFound_WhenIdIsInvalid()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var expectedUser = (User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1);

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			mockUserRepo.Setup(repo => repo.GetById(999))
				.ReturnsAsync((User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(999));

			var result = await controller.GetUserById(1);

			Assert.IsInstanceOf<NotFoundResult>(result);
		}

		[Test]
		public async Task Login_Successfully()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var hashedPassword = PasswordHasher.Encrypt("password");
			var returnedUser = (User)new User("John Doe", "johndoe", hashedPassword, "ADMIN").SetId(1);
			mockUserRepo.Setup(repo => repo.GetByUsername("johndoe"))
				.ReturnsAsync(returnedUser);

			var request = new LoginRequestDTO { Password = "password", Username = "johndoe" };

			var result = await controller.Login(request);
			Assert.IsInstanceOf<OkObjectResult>(result);

			var userResult = result as OkObjectResult;
			Assert.IsNotNull(userResult);
			Assert.AreEqual(returnedUser.Id, ((UserDTO)userResult!.Value).Id);
		}

		[Test]
		public async Task Login_Fail_WrongPassword()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var hashedPassword = PasswordHasher.Encrypt("ANOTHER PASSWORD");
			var returnedUser = (User)new User("John Doe", "johndoe", hashedPassword, "ADMIN").SetId(1);
			mockUserRepo.Setup(repo => repo.GetByUsername("johndoe"))
				.ReturnsAsync(returnedUser);

			var request = new LoginRequestDTO { Password = "password", Username = "johndoe" };

			var result = await controller.Login(request);
			Assert.IsInstanceOf<UnauthorizedResult>(result);
		}

		[Test]
		public async Task Login_Fail_InvalidUsername()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var hashedPassword = PasswordHasher.Encrypt("ANOTHER PASSWORD");
			var returnedUser = (User)new User("John Doe", "johndoe", hashedPassword, "ADMIN").SetId(1);
			mockUserRepo.Setup(repo => repo.GetByUsername("alex"))
				.ReturnsAsync(returnedUser);

			var request = new LoginRequestDTO { Password = "password", Username = "johndoe" };

			var result = await controller.Login(request);
			Assert.IsInstanceOf<NotFoundResult>(result);
		}

		[Test]
		public async Task GetAllUsers_Successfully()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var users = new List<User>
			{
				(User) new User("John Doe", "johndoe", "hashedpassword1", "ADMIN").SetId(1),
				(User) new User("Jane Smith", "janesmith", "hashedpassword2", "USER").SetId(2)
			};

			mockUserRepo.Setup(repo => repo.GetAllAsync())
				.ReturnsAsync(users);

			var result = await controller.GetAllUsers();
			Assert.IsInstanceOf<OkObjectResult>(result);

			var okResult = (result as OkObjectResult);
			Assert.IsNotNull(okResult);
		}

		[Test]
		public async Task DeleteUser_UserExists_ReturnsOk()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var userId = 1;

			mockUserRepo.Setup(repo => repo.DeleteAsync(userId))
				.ReturnsAsync((User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1));

			var result = await controller.DeleteUser(userId);

			Assert.IsInstanceOf<OkObjectResult>(result);

			var user = (UserDTO)(result as OkObjectResult).Value;
			Assert.IsNotNull(user);
			Assert.True(user.Id == userId);
		}

		[Test]
		public async Task DeleteUser_UserDoesNotExist_ReturnsNotFound()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var userId = 1;

			mockUserRepo.Setup(repo => repo.DeleteAsync(userId))
				.ReturnsAsync((User)null);

			var result = await controller.DeleteUser(userId);

			Assert.IsInstanceOf<NotFoundResult>(result);

			mockUserRepo.Verify(repo => repo.DeleteAsync(userId), Times.Once);
		}

		[Test]
		public async Task UpdateUser_ValidData_ReturnsOk()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var user = (User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1);
			var request = new UpdateUserRequestDTO
			{
				Name = "John Doe1",
				Username = "johndoe1",
				Password = "password1",
				Role = "ADMIN",
				Hospital = new HospitalDTO
				{
					Id = 1,
					Name = "Hospital 1"
				}
			};

			mockUserRepo.Setup(repo => repo.GetById(1))
				.ReturnsAsync(user);
			mockHospitalRepo.Setup(repo => repo.GetById(1))
				.ReturnsAsync((Hospital)new Hospital("Hospital", new OrderEntityContainer()).SetId(1));

			mockUserRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
				.ReturnsAsync((User)new User
				{
					Name = "John Doe1",
					Username = "johndoe1",
					Password = "password1",
					Role = "ADMIN",
					Hospital = (Hospital)new Hospital
					{
						Name = "Hospital 1"
					}.SetId(1)
				});

			var result = await controller.UpdateUser(1, request);
			Assert.IsInstanceOf<OkObjectResult>(result);
			var userResult = (UserDTO)(result as OkObjectResult).Value;
			Assert.IsNotNull(userResult);
			Assert.AreEqual(userResult.Name, request.Name);
			Assert.AreEqual(userResult.Username, request.Username);
			Assert.AreEqual(userResult.Hospital.Id, request.Hospital.Id);
		}

		[Test]
		public async Task UpdateUser_InvalidData_ReturnsBadRequest()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var user = (User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1);
			var request = new UpdateUserRequestDTO
			{
				Name = "John Doe1",
				Username = "johndoe1",
				Password = "password1",
				Role = "ADMIN",
				Hospital = new HospitalDTO
				{
					Id = 1,
					Name = "Hospital 1"
				}
			};

			mockUserRepo.Setup(repo => repo.GetById(1))
				.ReturnsAsync(user);
			mockHospitalRepo.Setup(repo => repo.GetById(99))
				.ReturnsAsync((Hospital)new Hospital("Hospital", new OrderEntityContainer()).SetId(1));

			mockUserRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
				.ReturnsAsync((User)new User
				{
					Name = "John Doe1",
					Username = "johndoe1",
					Password = "password1",
					Role = "ADMIN",
					Hospital = (Hospital)new Hospital
					{
						Name = "Hospital 1"
					}.SetId(1)
				});

			var result = await controller.UpdateUser(1, request);
			Assert.IsInstanceOf<BadRequestObjectResult>(result);
		}

		[Test]
		public async Task UpdateUser_InvalidId_ReturnsNotFound()
		{
			var mockUserRepo = new Mock<IUserRepository>();
			var mockPharmacyRepo = new Mock<IPharmacyRepository>();
			var mockHospitalRepo = new Mock<IHospitalRepository>();

			var controller = new UserController(mockUserRepo.Object, mockHospitalRepo.Object, mockPharmacyRepo.Object);

			var user = (User)new User("John Doe", "johndoe", "password", "ADMIN").SetId(1);
			var request = new UpdateUserRequestDTO
			{
				Name = "John Doe1",
				Username = "johndoe1",
				Password = "password1",
				Role = "ADMIN",
				Hospital = new HospitalDTO
				{
					Id = 1,
					Name = "Hospital 1"
				}
			};

			mockUserRepo.Setup(repo => repo.GetById(1))
				.ReturnsAsync(user);
			mockHospitalRepo.Setup(repo => repo.GetById(1))
				.ReturnsAsync((Hospital)new Hospital("Hospital", new OrderEntityContainer()).SetId(1));

			mockUserRepo.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
				.ReturnsAsync((User)new User
				{
					Name = "John Doe1",
					Username = "johndoe1",
					Password = "password1",
					Role = "ADMIN",
					Hospital = (Hospital)new Hospital
					{
						Name = "Hospital 1"
					}.SetId(1)
				});

			var result = await controller.UpdateUser(99, request);
			Assert.IsInstanceOf<NotFoundResult>(result);
		}
	}
}