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

builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
					 .AddEnvironmentVariables();


PasswordHasher.SetPasswordKey(Environment.GetEnvironmentVariable("pass"));

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
	options.UseSqlServer(Environment.GetEnvironmentVariable("conn"));
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
