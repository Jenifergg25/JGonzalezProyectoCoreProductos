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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
