using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Cafeteria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturacionService _facturacionService;

        public FacturasController(IFacturacionService facturacionService)
        {
            _facturacionService = facturacionService ?? throw new ArgumentNullException(nameof(facturacionService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDto>>> ObtenerTodasLasFacturas()
        {
            var facturas = await _facturacionService.ObtenerTodasLasFacturasAsync();
            return Ok(facturas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDto>> ObtenerFacturaPorId(int id)
        {
            var factura = await _facturacionService.ObtenerFacturaPorIdAsync(id);

            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearFactura(FacturaDto facturaDto)
        {
            var facturaId = await _facturacionService.CrearFacturaAsync(facturaDto);
            return CreatedAtAction(nameof(ObtenerFacturaPorId), new { id = facturaId }, facturaId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarFactura(int id, FacturaDto facturaDto)
        {
            if (id != facturaDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _facturacionService.ActualizarFacturaAsync(id, facturaDto);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFactura(int id)
        {
            try
            {
                await _facturacionService.EliminarFacturaAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
