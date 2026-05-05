using Microsoft.EntityFrameworkCore;
using RegistroAcademico.Core.Models;

namespace RegistroAcademico.Data.Services;

public class MateriaService
{
    private readonly AppDbContext _context;

    public MateriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Materia>> ObtenerTodos()
    {
        return await _context.Materias
            .Where(m => m.Activo)
            .ToListAsync();
    }

    public async Task<Materia?> ObtenerPorId(int id)
    {
        return await _context.Materias.FindAsync(id);
    }

    public async Task<Materia> Crear(Materia materia)
    {
        _context.Materias.Add(materia);
        await _context.SaveChangesAsync();
        return materia;
    }

    public async Task<Materia?> Actualizar(Materia materia)
    {
        var existente = await _context.Materias.FindAsync(materia.Id);
        if(existente == null) return null;

        existente.Nombre = materia.Nombre;
        existente.Codigo = materia.Codigo;
        existente.Creditos = materia.Creditos;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> Desactivar(int id)
    {
        var materia = await _context.Materias.FindAsync(id);
        if(materia == null) return false;
        materia.Activo = false;
        await _context.SaveChangesAsync();
        return true;
    }
}