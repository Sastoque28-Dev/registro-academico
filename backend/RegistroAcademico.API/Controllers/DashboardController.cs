using Microsoft.AspNetCore.Mvc;
using RegistroAcademico.Data.Services;

namespace RegistroAcademico.API.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _service;

    public DashboardController(DashboardService service)
    {
        _service = service;
    }

    [HttpGet("estadisticas")]
    public async Task<IActionResult> ObtenerEstadisticasGenerales()
    {
        var resultado = await _service.ObtenerEstadisticasGenerales();
        return Ok(resultado);
    }

    [HttpGet("top-mejores-promedios")]
    public async Task<IActionResult> ObtenerTopMejoresPromedios()
    {
        var resultado = await _service.ObtenerTopMejoresPromedios();
        return Ok(resultado);
    }

    [HttpGet("promedio-por-materia")]
    public async Task<IActionResult> ObtenerPromedioPorMateria()
    {
        var resultado = await _service.ObtenerPromedioPorMateria();
        return Ok(resultado);
    }
}