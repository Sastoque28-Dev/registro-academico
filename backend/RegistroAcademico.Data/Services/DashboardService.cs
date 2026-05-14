using Microsoft.EntityFrameworkCore;

namespace RegistroAcademico.Data.Services;

public class DashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> ObtenerEstadisticasGenerales()
    {
        var totalEstudiantes = await _context.Estudiantes
            .Where(e => e.Activo)
            .CountAsync();

        var totalMaterias = await _context.Materias
            .Where(m => m.Activo)
            .CountAsync();

        var totalCalificaciones = await _context.Calificaciones.CountAsync();

        var promedioGeneral = await _context.Calificaciones.AnyAsync()
            ? await _context.Calificaciones.AverageAsync(c => c.Nota)
            : 0;

        return new
        {
            totalEstudiantes,
            totalMaterias,
            totalCalificaciones,
            promedioGeneral = Math.Round(promedioGeneral, 2)
        };
    }

    public async Task<object> ObtenerTopMejoresPromedios()
{

    var calificaciones = await _context.Calificaciones
        .Include(c => c.Estudiante)
        .Where(c => c.Estudiante!.Activo)
        .ToListAsync();

    var resultado = calificaciones
        .GroupBy(c => c.EstudianteId)
        .Select(grupo => new
        {
            estudianteId = grupo.Key,
            nombreCompleto = grupo.First().Estudiante!.NombreCompleto,
            promedio = Math.Round(grupo.Average(c => c.Nota), 2),
            cantidadCalificaciones = grupo.Count()
        })
        .OrderByDescending(x => x.promedio)
        .Take(5)
        .ToList();

    return resultado;
}
   public async Task<object> ObtenerPromedioPorMateria()
{
    var calificaciones = await _context.Calificaciones
        .Include(c => c.Materia)
        .Where(c => c.Materia!.Activo)
        .ToListAsync();

    var resultado = calificaciones
        .GroupBy(c => c.MateriaId)
        .Select(grupo => new
        {
            materiaId = grupo.Key,
            nombreMateria = grupo.First().Materia!.Nombre,
            codigoMateria = grupo.First().Materia!.Codigo,
            promedio = Math.Round(grupo.Average(c => c.Nota), 2),
            cantidadCalificaciones = grupo.Count()
        })
        .OrderByDescending(x => x.promedio)
        .ToList();

    return resultado;
}
}