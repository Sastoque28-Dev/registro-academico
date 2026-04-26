using Microsoft.AspNetCore.Mvc;
using RegistroAcademico.Core.Models;
using RegistroAcademico.Data.Services;

namespace RegistroAcademico.API.Controllers;

[ApiController]
[Route("api/estudiantes")]
public class EstudiantesController : ControllerBase
{
    private readonly EstudianteService _service;

    public EstudiantesController(EstudianteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var estudiantes = await _service.ObtenerTodos();
        return Ok(estudiantes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
       var estudiante = await _service.ObtenerPorId(id);
       if (estudiante == null) return NotFound();
       return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Estudiante estudiante)
    {
        var creado = await _service.Crear(estudiante);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Estudiante estudiante)
    {
        if (id != estudiante.Id) return BadRequest();
        
        var actualizado = await _service.Actualizar(estudiante);
        if (actualizado == null) return NotFound();

        return Ok(actualizado);
    }

   [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var resultado = await _service.Desactivar(id);
        if (!resultado) return NotFound();
        return NoContent();
    }
}