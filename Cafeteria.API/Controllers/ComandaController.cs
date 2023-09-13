using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaService _comandaService;

        public ComandaController(IComandaService comandaService)
        {
            _comandaService = comandaService ?? throw new ArgumentNullException(nameof(comandaService));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasComandas()
        {
            var comandas = await _comandaService.ObtenerTodasLasComandasAsync();
            return Ok(comandas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerComandaPorId(int id)
        {
            var comanda = await _comandaService.ObtenerComandaPorIdAsync(id);
            if (comanda == null)
            {
                return NotFound(); // Retorna 404 si la comanda no se encuentra
            }
            return Ok(comanda);
        }

        [HttpPost]
        public async Task<IActionResult> CrearComanda([FromBody] ComandaDto comandaDto)
        {
            try
            {
                var nuevaComandaId = await _comandaService.CrearComandaAsync(comandaDto);
                return CreatedAtAction(nameof(ObtenerComandaPorId), new { id = nuevaComandaId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Retorna 400 en caso de error
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarComanda(int id, [FromBody] ComandaDto comandaDto)
        {
            try
            {
                await _comandaService.ActualizarComandaAsync(id, comandaDto);
                return NoContent(); // Retorna 204 si la actualización es exitosa
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Retorna 400 en caso de error
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarComanda(int id)
        {
            try
            {
                await _comandaService.EliminarComandaAsync(id);
                return NoContent(); // Retorna 204 si la eliminación es exitosa
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Retorna 400 en caso de error
            }
        }
    }
}
