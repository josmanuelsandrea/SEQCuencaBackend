using ScaneqCuencaBackend.DBModels;
using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.Mappings;
using ScaneqCuencaBackend.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SeqcuencabackendContext>(options => options.UseNpgsql((new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("DB").GetValue<string>("connection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<BusOrdersRepository>();
builder.Services.AddScoped<TruckOrdersRepository>();


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
