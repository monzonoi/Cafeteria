using Cafeteria.API.Request;
using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
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
        public async Task<ActionResult<IEnumerable<PedidoDto>>> ObtenerTodosLosPedidosAsync()
        {
            var pedidos = await _pedidoService.ObtenerTodosLosPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDto>> ObtenerPedidoPorIdAsync(int id)
        {
            var pedido = await _pedidoService.ObtenerPedidoPorIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost("CrearPedidoAsync")]
        public async Task<IActionResult> CrearPedidoAsync([FromBody] PedidoDto pedidoDto)
        {
            try
            {
                
                if (pedidoDto == null)
                {
                    return BadRequest("El objeto PedidoDto es inválido.");
                }
                
                int nuevoPedidoId = await _pedidoService.CrearPedidoAsync(pedidoDto);

                if (nuevoPedidoId > 0)
                {                
                    return Created($"pedido/{nuevoPedidoId}", nuevoPedidoId);
                }
                else
                {                 
                    return BadRequest("No se pudo crear el pedido.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPedidoAsync(int id, [FromBody] PedidoDto pedidoDto)
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
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPedidoAsync(int id)
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
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("EditarPedidoAsync")]
        public async Task<IActionResult> EditarPedidoAsync([FromBody] EditarPedidoRequest request)
        {
            try
            {
               
                if (request.Usuario == null || request.Pedido == null)
                {
                    return BadRequest("Los objetos UsuarioDto y PedidoDto son inválidos.");
                }

                
                bool resultadoEdicion = await _pedidoService.EditarPedidoAsync(request.Usuario, request.Pedido);

                if (resultadoEdicion)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("No se pudo editar el pedido.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
