// Don't we have a class? Let's take a look with a Decompiler. 
using NetCoreCourse.FirstExample.WebApp.Configuration;
using NetCoreCourse.FirstExample.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

//Adding first configuration object.
var firstConfigurationObject = builder.Configuration.GetSection("FirstConfiguration");
builder.Services.Configure<FirstConfigurationOptions>(firstConfigurationObject);

//Dependency Injection
builder.Services.AddTransient<IForecastService, ForecastService>();
builder.Services.AddTransient<IServiceUsingServices, ServiceUsingServices>();

builder.Services.AddTransient<ITransientRandomValueService, RandomValueService>();
builder.Services.AddScoped<IScopedRandomValueService, RandomValueService>();
builder.Services.AddSingleton<ISingletonRandomValueService, RandomValueService>();

//Building App
var app = builder.Build();

// Configure the HTTP request pipelines. MIDDLEWARES.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
//Adding a New Custom Environment
if (app.Environment.IsEnvironment("MarcosDev"))
{
    app.Logger.LogInformation("Este es el ambiente de Marcos.");
}
//TODO Check this ---> Funciona pero ver la diferencia entre uno y el otro
//app.UseHsts(); // It returns a header that tells the browser to always try to do the first request over HTTPS.
//app.UseHttpsRedirection();

app.UseStaticFiles(); //img/logo.jpg

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
//Minimal API Definition. More info on: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0
app.MapGet("/api/firstapi", () => "Hey here is your first API!");

app.MapControllerRoute(
       name: "default",
       pattern: "{controller}/{action=Index}/{id?}");

app.Run();
