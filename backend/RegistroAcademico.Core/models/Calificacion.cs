using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroAcademico.Core.Models;

public class Calificacion
{
    public int Id { get; set;}

    public int EstudianteId { get; set;}

    public Estudiante? Estudiante { get; set;}

    public int MateriaId { get; set;}

    public Materia? Materia { get; set;}

    [Column(TypeName = "decimal(3,1)")] // 3 Digitos maximo, decimal despues del primer numero
    public decimal Nota { get; set;}

    [Required]
    [MaxLength(10)]
    public required string Periodo { get; set;}

    public DateTime FechaRegistro { get; set;} = DateTime.Now;
}