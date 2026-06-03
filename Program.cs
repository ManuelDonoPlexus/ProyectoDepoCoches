using Microsoft.EntityFrameworkCore;
using CarDepo.Infrastructure.Data;
using CarDepo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarDepo.Application.Utils;
using CarDepo.Application.Services;

// Aquí empieza la construcción de la aplicación, añadiendo a los servicios de la misma los controladores, la API y el contexto de la Base de Datos.

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<CarDepoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CarDepoContext")));

// A continuación se añaden a la base de datos multiples servicios para el funcionamiento de la aplicación.

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

// Se añade autenticicación de la aplicación, aplicando varios parametros como la clave JWT, la audiencia, el proporcionador, etc.
// La clave Jwt, el token usado para ejecutar acciones con autorización requerida, se encuentra en 'appsettings.json'

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

// Se añade una politicas al Cors (mecanismo de seguridad para aplicaciones web), permitiendo cualquier header, metodo o origen para ser lanzado contra la aplicación

builder.Services.AddCors(options =>
{
    options.AddPolicy("New Policy", app =>
    {
        app.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// Comienza la construcción de la aplicación

var app = builder.Build();

// Si el entorno de la aplicación es de desarrollo, se mapea a OpenAPI y se utiliza Swagger.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

// Se especifica en la aplicación el uso de wwwroot (UseDefaultFiles y UseStaticFiles), se añade intermidiarios para redirecciones Https, se aplica la politica antes establecida, se activa la autorización y se mapean los controladores.

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("New Policy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Aquí se ejecura la aplicación

app.Run();