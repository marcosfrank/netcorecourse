// No tenemos una clase. Como es posible? Utilicemos un decompilador.
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using NetCoreCourse.FirstExample.WebApp.Configuration;
using NetCoreCourse.FirstExample.WebApp.DataAccess;
using NetCoreCourse.FirstExample.WebApp.Services;
using System.Security.Cryptography.Xml;
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
) ;

//Agregando el primer objeto de configuracion.
var firstConfigurationObject = builder.Configuration.GetSection("FirstConfiguration");
builder.Services.Configure<FirstConfigurationOptions>(firstConfigurationObject);

// Agregamos los servicios al contenedor de dependencias
builder.Services.AddTransient<IForecastService, ForecastService>();
builder.Services.AddTransient<IServiceUsingServices, ServiceUsingServices>();

builder.Services.AddTransient<ITransientRandomValueService, RandomValueService>();
builder.Services.AddScoped<IScopedRandomValueService, RandomValueService>();
builder.Services.AddSingleton<ISingletonRandomValueService, RandomValueService>();

//Lo vamos a ver en el Modulo de EF Core
//builder.Services.AddDbContext<ThingsContext>(options =>
//{
//    //Para poder utilizar SqlServer necesitamos instalar el paquete
//    //Microsoft.EntityFrameworkCore.SqlServer
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ThingsContextConnection"));
//});

//Creando la aplicacion.
var app = builder.Build();

// Configurando el "pipeline" para las peticiones "HTTP". MIDDLEWARES.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
//Probando nuevos ambientes.
if (app.Environment.IsEnvironment("MarcosDev"))
{
    app.Logger.LogInformation("Este es el ambiente de Marcos.");
}
//TODO TODO TODO Check this ---> Funciona pero ver la diferencia entre uno y el otro
//app.UseHsts(); // It returns a header that tells the browser to always try to do the first request over HTTPS.
//app.UseHttpsRedirection();

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
