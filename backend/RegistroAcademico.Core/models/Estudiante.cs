using System.ComponentModel.DataAnnotations;

namespace RegistroAcademico.Core.Models;

public class Estudiante
{
    public int Id { get; set;}

    [Required] // NOT NULL
    [MaxLength(200)] // NVARCHAR(200)
    public required string NombreCompleto { get; set; }

    [Required]
    [MaxLength(20)]
    public required string NumeroIdentificacion { get; set; }

    [Required]
    [MaxLength(150)]
    public required string CorreoElectronico { get; set; }

    [Required]
    public DateTime FechaNacimiento { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

}