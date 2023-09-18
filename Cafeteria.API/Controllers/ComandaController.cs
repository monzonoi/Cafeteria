using Cafeteria.API.Request;
using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

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


        ///*generadas luego de Mock*/


        [HttpPut("{id:int}/CambiarEstado")]
        public async Task<IActionResult> CambiarEstadoComandaAsync(int id, [FromBody] CambiarEstadoRequest request)
        {
            try
            {
                await _comandaService.CambiarEstadoComandaAsync(id, request.NuevoEstado);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasComandasAsync()
        {
            try
            {
                var comandaDtos = await _comandaService.ObtenerTodasLasComandasAsync();

                return Ok(comandaDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerComandaPorIdAsync(int comandaId)
        {
            try
            {
                var comandaDto = await _comandaService.ObtenerComandaPorIdAsync(comandaId);

                if (comandaDto == null)
                {
                    return NotFound();
                }

                return Ok(comandaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearComandaAsync([FromBody] ComandaDto comandaDto)
        {
            try
            {
                var comandaId = await _comandaService.CrearComandaAsync(comandaDto);
                return CreatedAtAction(nameof(ObtenerComandaPorIdAsync), new { comandaId }, comandaId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:int}/Comanda")]
        public async Task<IActionResult> ActualizarComandaAsync(int id, [FromBody] ComandaDto comandaDto)
        {
            try
            {
                var resultado = await _comandaService.ActualizarComandaAsync(id, comandaDto);

                if (resultado)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("La comanda no fue encontrada.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("ObtenerTrabajosRealizadosAsync")]
        public async Task<IActionResult> ObtenerTrabajosRealizadosAsync([FromBody] UsuarioDto usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest("Los datos del usuario son inválidos.");
                }

                var trabajosRealizados = await _comandaService.ObtenerTrabajosRealizadosAsync(usuario);

                return Ok(trabajosRealizados);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CambiarEstadoPedidoAsync")]
        public async Task<IActionResult> CambiarEstadoPedidoAsync([FromBody] CambiarEstadoPedidoRequest request)
        {
            try
            {
                if (request.PedidoDto == null || string.IsNullOrWhiteSpace(request.NuevoEstado))
                {
                    return BadRequest("Los datos del pedido o el nuevo estado son inválidos.");
                }

                await _comandaService.CambiarEstadoPedidoAsync(request.PedidoDto, request.NuevoEstado);

                return Ok("Estado del pedido cambiado con éxito.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("EditarComandaAsync")]
        public async Task<IActionResult> EditarComandaAsync([FromBody] EditarComandaRequest request)

        {
            try
            {
                if (request.ComandaDto == null || request.Usuario == null)
                {
                    return BadRequest("Los datos de la comanda o el usuario son inválidos.");
                }

                var exito = await _comandaService.EditarComandaAsync(request.Usuario, request.ComandaDto);

                if (exito)
                {
                    return Ok("Comanda editada con éxito.");
                }
                else
                {
                    return NotFound("Comanda no encontrada.");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }


}
