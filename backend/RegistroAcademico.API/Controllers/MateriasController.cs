using Microsoft.AspNetCore.Mvc;
using RegistroAcademico.Core.Models;
using RegistroAcademico.Data.Services;

namespace RegistroAcademico.API.Controllers;

[ApiController]
[Route("api/materias")]
public class MateriasController : ControllerBase
{
    private readonly MateriaService _service;

    public MateriasController(MateriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var materias = await _service.ObtenerTodos();
        return Ok(materias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var materia = await _service.ObtenerPorId(id);
        if(materia == null) return NotFound();
        return Ok(materia);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Materia materia)
    {
        var creada = await _service.Crear(materia);
        return CreatedAtAction(nameof(ObtenerPorId), new {id = creada.Id}, creada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Materia materia)
    {
        if(id != materia.Id) return BadRequest();

        var actualizada = await _service.Actualizar(materia);
        if (actualizada == null) return NotFound();

        return Ok(actualizada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var resultado = await _service.Desactivar(id);
        if (!resultado) return NotFound();
        return NoContent();
    }
}