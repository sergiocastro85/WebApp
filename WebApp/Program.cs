using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Servicios;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//connfigurar el servicio
builder.Services.AddTransient<IRepositoriosCategorias, RepositoriosCategorias>();
//configuar el servicio de usuairo
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
//servicio Articulo
builder.Services.AddTransient<IRepositorioArticulos, RepositorioArticulos>();
//configuara AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
//Proveedores
builder.Services.AddTransient<IRepositorioProveedor, RepositorioProveedor>();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
