using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class MateriaPrimaController : ControllerBase
{
    private readonly IMateriaPrimaService _materiaPrimaService;

    public MateriaPrimaController(IMateriaPrimaService materiaPrimaService)
    {
        _materiaPrimaService = materiaPrimaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodasLasMateriasPrimas()
    {
        var materiasPrimas = await _materiaPrimaService.ObtenerTodasLasMateriasPrimasAsync();
        return Ok(materiasPrimas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerMateriaPrimaPorId(int id)
    {
        var materiaPrima = await _materiaPrimaService.ObtenerMateriaPrimaPorIdAsync(id);
        if (materiaPrima == null)
        {
            return NotFound(); // Retorna 404 si la materia prima no se encuentra
        }
        return Ok(materiaPrima);
    }

    [HttpPost]
    public async Task<IActionResult> CrearMateriaPrima([FromBody] MateriaPrimaDto materiaPrimaDto)
    {
        try
        {
            var nuevaMateriaPrimaId = await _materiaPrimaService.CrearMateriaPrimaAsync(materiaPrimaDto);
            return CreatedAtAction(nameof(ObtenerMateriaPrimaPorId), new { id = nuevaMateriaPrimaId }, null);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Retorna 400 en caso de error
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarMateriaPrima(int id, [FromBody] MateriaPrimaDto materiaPrimaDto)
    {
        try
        {
            await _materiaPrimaService.ActualizarMateriaPrimaAsync(id, materiaPrimaDto);
            return NoContent(); // Retorna 204 si la actualización es exitosa
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Retorna 400 en caso de error
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarMateriaPrima(int id)
    {
        try
        {
            await _materiaPrimaService.EliminarMateriaPrimaAsync(id);
            return NoContent(); // Retorna 204 si la eliminación es exitosa
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Retorna 400 en caso de error
        }
    }
}