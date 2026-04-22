using Microsoft.EntityFrameworkCore;
using CarDepo.Infrastructure.Data;
using CarDepo.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<CarDepoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CarDepoContext")));
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarDriverRepository, CarDriverRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IFineRepository, FineRepository>();
builder.Services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();
builder.Services.AddScoped<IMakeRepository, MakeRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();