using Microsoft.EntityFrameworkCore;
using RegistroAcademico.Core.Models;

namespace RegistroAcademico.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Calificacion> Calificaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Estudiantes: cédula y correo deben ser únicos
        modelBuilder.Entity<Estudiante>()
            .HasIndex(e => e.NumeroIdentificacion)
            .IsUnique();

        modelBuilder.Entity<Estudiante>()
            .HasIndex(e => e.CorreoElectronico)
            .IsUnique();

        // Materias: código único
        modelBuilder.Entity<Materia>()
            .HasIndex(m => m.Codigo)
            .IsUnique();

        // Calificaciones: un estudiante no puede tener dos notas en la misma materia y período
        modelBuilder.Entity<Calificacion>()
            .HasIndex(c => new { c.EstudianteId, c.MateriaId, c.Periodo })
            .IsUnique();

        // Calificaciones: la nota debe estar entre 0.0 y 5.0
        modelBuilder.Entity<Calificacion>()
            .ToTable(t => t.HasCheckConstraint("CK_Calificacion_Nota", "[Nota] >= 0.0 AND [Nota] <= 5.0"));

        // Materias: créditos deben ser positivos
        modelBuilder.Entity<Materia>()
            .ToTable(t => t.HasCheckConstraint("CK_Materia_Creditos", "[Creditos] > 0"));
    }
}