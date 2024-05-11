using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TiendaServicios.Api.Gateway.ImplementRemote;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.MessajeHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.AddOcelot();
//builder.Configuration
//  .AddJsonFile("ocelot.json");
// .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<IAutorRemote, AutorRemote>();

builder.Services.AddHttpClient("AutorService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Autor"]);
});



builder.Services.AddOcelot().AddDelegatingHandler<LibroHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
//await app.UseOcelot();
app.UseOcelot().Wait();
app.MapControllers();

app.Run();
