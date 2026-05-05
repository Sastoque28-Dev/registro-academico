using Microsoft.AspNetCore.Mvc;
using RegistroAcademico.Core.Models;
using RegistroAcademico.Data.Services;

namespace RegistroAcademico.API.Controllers;

[ApiController]
[Route("api/calificaciones")]
public class CalificacionesController : ControllerBase
{
    private readonly CalificacionService _service;

    public CalificacionesController(CalificacionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var calificaciones = await _service.ObtenerTodos();
        return Ok(calificaciones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var calificacion = await _service.ObtenerPorId(id);
        if (calificacion == null) return NotFound();
        return Ok(calificacion);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Calificacion calificacion)
    {
        var (creada, error) = await _service.Crear(calificacion);
        if (error != null) return BadRequest(new { mensaje = error });
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada!.Id }, creada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Calificacion calificacion)
    {
        if (id != calificacion.Id) return BadRequest(new { mensaje = "El ID de la URL no coincide con el del cuerpo." });

        var (actualizada, error) = await _service.Actualizar(calificacion);
        if (error != null) return BadRequest(new { mensaje = error });
        return Ok(actualizada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var resultado = await _service.Eliminar(id);
        if (!resultado) return NotFound();
        return NoContent();
    }
}