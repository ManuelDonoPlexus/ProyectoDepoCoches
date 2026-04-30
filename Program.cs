using Microsoft.EntityFrameworkCore;
using CarDepo.Infrastructure.Data;
using CarDepo.Infrastructure.Repositories;

// Builder, encargada de especificar varios áspectos de la aplicación 
var builder = WebApplication.CreateBuilder(args);

// Añade los controladores de la aplicación
builder.Services.AddControllers();

// Añade los servicios de OpenApi (un conjunto de reglas y especificaciones basadas en estandares web) 
builder.Services.AddOpenApi();

// Añade el contexto de la base de datos a la aplicación, configurando la conexión a la misma y definiendo el tipo de base que se usa
// El contexto es una clase encargada de la gestión de la sesión
builder.Services.AddDbContext<CarDepoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CarDepoContext")));

// Para añadir un servicio especifico a la aplicación. En este caso, añadimos los servicios de la interfaz del repositorio y el propio repositorio.
/*
Para añadir un servicio, existen tres formas de hacerlo:
    -> Transient:    se crea una nueva instancia por cada petición
    -> Singleton:    se crea una única instancia para todas las peticiones 
    -> Scoped:       son la misma instancia, pero diferentes llamadas de la petición
*/

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarDriverRepository, CarDriverRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IFineRepository, FineRepository>();
builder.Services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();
builder.Services.AddScoped<IMakeRepository, MakeRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

// La aplicación construida con todos los parametros anteriores
var app = builder.Build();

// Si el entorno de la aplicación es el de Desarrollo, se mapea OpenAPI y se usa SwaggerUI para la realización de pruebas
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

// Especificaciones de la aplicación adicionales:
// - Usar el mapeo por defecto
// - Usar los archivos de WebRootPath, por defecto wwwroot
// - Usar redirección de HTTPS
// - Activa las capacidades de autorización
// - Mapear las acciones de los controladores

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ejecutar la aplicación
app.Run();