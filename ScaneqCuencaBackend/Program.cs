using ScaneqCuencaBackend.DBModels;
using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get the environment (Development, Staging, Production)
var environment = builder.Environment.EnvironmentName;

// Load the configuration from appsettings.json and appsettings.{Environment}.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .Build();

// Add DbContext with the connection string from the correct settings file
builder.Services.AddDbContext<SeqcuencabackendContext>(options =>
    options.UseNpgsql(configuration.GetSection("DB").GetValue<string>("connection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();