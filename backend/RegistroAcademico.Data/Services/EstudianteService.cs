using Microsoft.EntityFrameworkCore;
using RegistroAcademico.Core.Models;

namespace RegistroAcademico.Data.Services;

public class EstudianteService
{
    private readonly AppDbContext _context;

    public EstudianteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Estudiante>> ObtenerTodos()
    {
        return await _context.Estudiantes
        .Where(e => e.Activo)
        .ToListAsync();
    }


    public async Task<Estudiante?> ObtenerPorId(int id)
    {
        return await _context.Estudiantes
        .FindAsync(id);
    }

    public async Task<Estudiante> Crear(Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();
        return estudiante;
    }

    public async Task<Estudiante?> Actualizar(Estudiante estudiante)
    {
        var existente = await _context.Estudiantes.FindAsync(estudiante.Id);
        if (existente == null) return null;

        existente.NombreCompleto = estudiante.NombreCompleto;
        existente.NumeroIdentificacion = estudiante.NumeroIdentificacion;
        existente.CorreoElectronico = estudiante.CorreoElectronico;
        existente.FechaNacimiento = estudiante.FechaNacimiento;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> Desactivar(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante == null) return false;
        estudiante.Activo = false;
        await _context.SaveChangesAsync();
        return true;
    }
  
}