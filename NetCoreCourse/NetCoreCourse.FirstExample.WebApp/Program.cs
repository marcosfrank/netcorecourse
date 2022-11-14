// No tenemos una clase. Como es posible? Utilicemos un decompilador.
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetCoreCourse.FirstExample.WebApp.Configuration;
using NetCoreCourse.FirstExample.WebApp.DataAccess;
using NetCoreCourse.FirstExample.WebApp.Dto;
using NetCoreCourse.FirstExample.WebApp.Filters;
using NetCoreCourse.FirstExample.WebApp.Handlers;
using NetCoreCourse.FirstExample.WebApp.Services;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => {
    options.Filters.Add<NetCoreCourseFilter>();
})
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//Modulo API
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

//Agregando el primer objeto de configuracion.
var firstConfigurationObject = builder.Configuration.GetSection("FirstConfiguration");
builder.Services.Configure<FirstConfigurationOptions>(firstConfigurationObject);
//Agregando configuracion para JWT
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

// Agregamos los servicios al contenedor de dependencias
builder.Services.AddTransient<IForecastService, ForecastService>();
builder.Services.AddTransient<IServiceUsingServices, ServiceUsingServices>();
builder.Services.AddTransient<IMinimalApiService, MinimalApiService>();

builder.Services.AddTransient<ITransientRandomValueService, RandomValueService>();
builder.Services.AddScoped<IScopedRandomValueService, RandomValueService>();
builder.Services.AddSingleton<ISingletonRandomValueService, RandomValueService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IJwtHandler, JwtHandler>();

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

app.UseRouting();

//Modulo API - Middleware para utilizar Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

//Definicion de "Minimal API". Mas info en: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0
app.MapGet("/api/firstapi", () => "Hey here is your first API!");

app.MapPost("/api/minimalapi", (MinimalApiRequest request, IMinimalApiService service) => {
    return service.Execute(request);
});

app.MapControllerRoute(
       name: "default",
       pattern: "{controller}/{action=Index}/{id?}");

app.Run();