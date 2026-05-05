using RegistroAcademico.Data.Services;
using Microsoft.EntityFrameworkCore;
using RegistroAcademico.Data;

var builder = WebApplication.CreateBuilder(args);

// Servicios de la aplicación
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro del DbContext con la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios de negocio
builder.Services.AddScoped<EstudianteService>();
builder.Services.AddScoped<MateriaService>();
builder.Services.AddScoped<CalificacionService>();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();