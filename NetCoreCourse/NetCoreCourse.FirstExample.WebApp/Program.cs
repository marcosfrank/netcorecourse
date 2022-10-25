// No tenemos una clase. Como es posible? Utilicemos un decompilador.
using Microsoft.EntityFrameworkCore;
using NetCoreCourse.FirstExample.WebApp.Configuration;
using NetCoreCourse.FirstExample.WebApp.DataAccess;
using NetCoreCourse.FirstExample.WebApp.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//Agregamos las paginas de Razor. Que son? Las vamos a ver en el modulo de MVC.
builder.Services.AddRazorPages();

//Lo vamos a ver en el Modulo de EF Core
//Agregamos controllers y configuramos el serializador de JSON.
// Esta configuracion de Ignorar Ciclos solo debemos realizarlo ya que utilizamos las entidades de EF Core como respuesta de la API.
// Normalmente no lo necesitariamos.
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }
);

//Agregando el primer objeto de configuracion.
var firstConfigurationObject = builder.Configuration.GetSection("FirstConfiguration");
builder.Services.Configure<FirstConfigurationOptions>(firstConfigurationObject);

// Agregamos los servicios al contenedor de dependencias
builder.Services.AddTransient<IForecastService, ForecastService>();
builder.Services.AddTransient<IServiceUsingServices, ServiceUsingServices>();

builder.Services.AddTransient<ITransientRandomValueService, RandomValueService>();
builder.Services.AddScoped<IScopedRandomValueService, RandomValueService>();
builder.Services.AddSingleton<ISingletonRandomValueService, RandomValueService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Lo vamos a ver en el Modulo de EF Core
builder.Services.AddDbContext<ThingsContext>(options =>
{
    //Para poder utilizar SqlServer necesitamos instalar el paquete
    //Microsoft.EntityFrameworkCore.SqlServer
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThingsContextConnection"));
});

//Creando la aplicacion.
var app = builder.Build();

// Configurando el "pipeline" para las peticiones "HTTP". MIDDLEWARES.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    //app.UseHsts(); // Retorna un header que le dice a los clientes que siempre intenten realizar el primer request con HTTPS.
    // El metodo anterior no es recomendado para ambientes NO productivos ya que son cacheados por los navegadores.
}
//Probando nuevos ambientes.
if (app.Environment.IsEnvironment("MarcosDev"))
{
    app.Logger.LogInformation("Este es el ambiente de Marcos.");
}

//app.UseHttpsRedirection(); //Redirecciona cualquier request HTTP a HTTPS

app.UseStaticFiles(); //img/logo.jpg

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
//Definicion de "Minimal API". Mas info en: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0
app.MapGet("/api/firstapi", () => "Hey here is your first API!");

app.MapControllerRoute(
       name: "default",
       pattern: "{controller}/{action=Index}/{id?}");

app.Run();