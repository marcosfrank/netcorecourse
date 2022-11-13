using NetCoreCourse.MvcApp.Protos;
using NetCoreCourse.MvcApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IAlumnoService, AlumnoService>(); //Singleton solo para que queden los datos que agregamos en memoria.
builder.Services.AddScoped<IDelayService, DelayService>();
builder.Services.AddHttpClient();

builder.Services.AddGrpc(opt => {
    opt.EnableDetailedErrors = true; //Esto nos permite tener errores detallados
});
builder.Services.AddGrpcReflection();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.MapGrpcReflectionService();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGrpcService<GrpcNetCoreCourseService>();
app.MapGrpcService<GrpcProtoAlumnoService>();

app.Run();