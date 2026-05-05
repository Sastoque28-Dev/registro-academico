using Microsoft.EntityFrameworkCore;
using RegistroAcademico.Core.Models;

namespace RegistroAcademico.Data.Services;

public class CalificacionService
{
    private readonly AppDbContext _context;

    public CalificacionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Calificacion>> ObtenerTodos()
    {
        return await _context.Calificaciones
            .Include(c => c.Estudiante)
            .Include(c => c.Materia)
            .ToListAsync();
    }

    public async Task<Calificacion?> ObtenerPorId(int id)
    {
        return await _context.Calificaciones
            .Include(c => c.Estudiante)
            .Include(c => c.Materia)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<(Calificacion? calificacion, string? error)> Crear(Calificacion calificacion)
    {
        var estudiante = await _context.Estudiantes.FindAsync(calificacion.EstudianteId);
        if (estudiante == null || !estudiante.Activo)
            return (null, "El estudiante no existe o está inactivo.");

        var materia = await _context.Materias.FindAsync(calificacion.MateriaId);
        if (materia == null || !materia.Activo)
            return (null, "La materia no existe o está inactiva.");

        _context.Calificaciones.Add(calificacion);
        await _context.SaveChangesAsync();
        return (calificacion, null);
    }

    public async Task<(Calificacion? calificacion, string? error)> Actualizar(Calificacion calificacion)
    {
        var existente = await _context.Calificaciones.FindAsync(calificacion.Id);
        if (existente == null) return (null, "La calificación no existe.");

        var estudiante = await _context.Estudiantes.FindAsync(calificacion.EstudianteId);
        if (estudiante == null || !estudiante.Activo)
            return (null, "El estudiante no existe o está inactivo.");

        var materia = await _context.Materias.FindAsync(calificacion.MateriaId);
        if (materia == null || !materia.Activo)
            return (null, "La materia no existe o está inactiva.");

        existente.EstudianteId = calificacion.EstudianteId;
        existente.MateriaId = calificacion.MateriaId;
        existente.Nota = calificacion.Nota;
        existente.Periodo = calificacion.Periodo;

        await _context.SaveChangesAsync();
        return (existente, null);
    }

    public async Task<bool> Eliminar(int id)
    {
        var calificacion = await _context.Calificaciones.FindAsync(id);
        if (calificacion == null) return false;
        _context.Calificaciones.Remove(calificacion);
        await _context.SaveChangesAsync();
        return true;
    }
}