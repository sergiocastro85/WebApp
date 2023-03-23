using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Models;
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
//DetallesVentas
builder.Services.AddTransient<IRepositorioDetalleVentas, RepositorioDetallesVentas>();
//ReporteProveedores
builder.Services.AddTransient<IRepositorioStock, RepositorioStockProducto>();
builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStore>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<SignInManager<Usuario>>();

builder.Services.AddIdentityCore<Usuario>(opciones =>
{
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireUppercase = false;
    opciones.Password.RequireNonAlphanumeric = false;

});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme);



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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
