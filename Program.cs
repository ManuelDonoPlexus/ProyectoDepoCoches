using Microsoft.EntityFrameworkCore;
using CarDepo.Infrastructure.Data;
using CarDepo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarDepo.API.Utils;
using CarDepo.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<CarDepoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CarDepoContext")));

// REPOSITORIOS

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarDriverRepository, CarDriverRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IFineRepository, FineRepository>();
builder.Services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();
builder.Services.AddScoped<IMakeRepository, MakeRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IStadisticsRepository, StadisticsRepository>();

// SERVICIOS

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthUtilities>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<CarDriverService>();
builder.Services.AddScoped<ColorService>();
builder.Services.AddScoped<DriverService>();
builder.Services.AddScoped<FineService>();
builder.Services.AddScoped<FuelTypeService>();
builder.Services.AddScoped<MakeService>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<StadisticsService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "cardepo.company",
            ValidAudience = "cardepo.company",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("New Policy", app =>
    {
        app.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

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
app.UseCors("New Policy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();