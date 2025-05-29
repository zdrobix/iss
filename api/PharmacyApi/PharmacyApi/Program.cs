using Serilog;
using Microsoft.EntityFrameworkCore;
using PharmacyApi.Data;
using PharmacyApi.Repo.Implementation;
using PharmacyApi.Repo.Interface;
using PharmacyApi.Utils;
using System.Text.Json.Serialization;
using System.Collections;

Log.Logger = new LoggerConfiguration()
	.WriteTo.File("logs/log.txt")
	.CreateLogger();

Log.Information("Application started.");

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

Console.WriteLine($"ENV Password = {Environment.GetEnvironmentVariable("Keys__Password")}");
Console.WriteLine($"ENV ConnStr = {Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")}");


PasswordHasher.SetPasswordKey(builder.Configuration["Keys:Password"]!);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IDrugStorageRepository, DrugStorageRepository>();
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IStoredDrugRepository, StoredDrugRepository>();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}
Console.WriteLine($"Connection string: {builder.Configuration.GetConnectionString("DefaultConnection")}");
Console.WriteLine($"Password key: {builder.Configuration["Keys:Password"]}");

app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseCors(options => {
	options.AllowAnyHeader();
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
