using System.ComponentModel.DataAnnotations;

namespace RegistroAcademico.Core.Models;

public class Materia
{
    public int Id { get; set;}

    [Required]
    [MaxLength(100)]
    public required string Nombre { get; set; }
    
    [Required]
    [MaxLength(10)]
    public required string Codigo { get; set; }

    [Required]
    public int Creditos { get; set; }

    public bool Activo { get; set;} = true;
}