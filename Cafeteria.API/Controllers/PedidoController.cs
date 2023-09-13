using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Cafeteria.Domain.Entidad;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> ObtenerTodosLosPedidos()
        {
            var pedidos = await _pedidoService.ObtenerTodosLosPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDto>> ObtenerPedidoPorId(int id)
        {
            var pedido = await _pedidoService.ObtenerPedidoPorIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearPedido([FromBody] PedidoDto pedidoDto)
        {
            try
            {
                var pedidoId = await _pedidoService.CrearPedidoAsync(pedidoDto);
                return CreatedAtAction(nameof(ObtenerPedidoPorId), new { id = pedidoId }, pedidoId);
            }
            catch (Exception ex)
            {
                // Manejo de errores y devolución de una respuesta de error adecuada
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPedido(int id, [FromBody] PedidoDto pedidoDto)
        {
            try
            {
                await _pedidoService.ActualizarPedidoAsync(id, pedidoDto);
                return NoContent();
            }
            catch (ApplicationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Manejo de errores y devolución de una respuesta de error adecuada
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            try
            {
                await _pedidoService.EliminarPedidoAsync(id);
                return NoContent();
            }
            catch (ApplicationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Manejo de errores y devolución de una respuesta de error adecuada
                return BadRequest(ex.Message);
            }
        }
    }
}
