using Serilog;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Repo.Implementation;
using PharmacyApi.Repo.Interface;
using PharmacyApi.Utils;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

Log.Logger = new LoggerConfiguration()
	.WriteTo.File("logs/log.txt")
	.CreateLogger();

Log.Information("Application started.");

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();


PasswordHasher.SetPasswordKey(Environment.GetEnvironmentVariable("pass")!);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(Environment.GetEnvironmentVariable("conn"));
});

builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IDrugStorageRepository, DrugStorageRepository>();
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IStoredDrugRepository, StoredDrugRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	var secretKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("pass")! + "" + Environment.GetEnvironmentVariable("pass")!);

	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = Environment.GetEnvironmentVariable("issuer"),
		ValidAudience = Environment.GetEnvironmentVariable("audience"),
		IssuerSigningKey = new SymmetricSecurityKey(secretKey)
	};
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("https://iss-production.up.railway.app", "https://zdrobix.github.io", "https://f2027a356901.ngrok-free.app" )
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(options =>
{
	options.AllowAnyHeader();
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
