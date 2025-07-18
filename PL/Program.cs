using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Inyeccion cadena de conexion
builder.Services.AddDbContext<JgonzalezProgramacionNcapasContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("JGonzalezProgramacionNCapas")));

//Inyeccion BL
builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Estado>();
builder.Services.AddScoped<BL.Municipio>();
builder.Services.AddScoped<BL.Colonia>();
builder.Services.AddScoped<BL.Producto>();
builder.Services.AddScoped<BL.Categoria>();
builder.Services.AddScoped<BL.SubCategoria>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
