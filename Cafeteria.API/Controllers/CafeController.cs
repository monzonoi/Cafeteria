using Microsoft.AspNetCore.Mvc;
using Cafeteria.Application; // Asegúrate de importar el espacio de nombres de la aplicación
using Cafeteria.Application.Service;
using Cafeteria.Domain;
using System.Net;
using Cafeteria.Application.Dtos;

namespace Cafeteria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : Controller
    {

        private readonly ICafeService _cafeService;

        public CafeController(ICafeService cafeService)
        {
            _cafeService = cafeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CafeDto>>> ObtenerTodosLosCafes()
        {
            var cafes = await _cafeService.ObtenerTodosLosCafesAsync();
            return Ok(cafes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CafeDto>> ObtenerCafePorId(int id)
        {
            var cafe = await _cafeService.ObtenerCafePorIdAsync(id);
            if (cafe == null)
            {
                return NotFound();
            }
            return Ok(cafe);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearCafe([FromBody] CafeDto cafeDto)
        {
            try
            {
                var cafeId = await _cafeService.CrearCafeAsync(cafeDto);
                return CreatedAtAction(nameof(ObtenerCafePorId), new { id = cafeId }, cafeId);
            }
            catch (Exception ex)
            {
                // Manejo de errores y devolución de una respuesta de error adecuada
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCafe(int id, [FromBody] CafeDto cafeDto)
        {
            try
            {
                await _cafeService.ActualizarCafeAsync(id, cafeDto);
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
        public async Task<IActionResult> EliminarCafe(int id)
        {
            try
            {
                await _cafeService.EliminarCafeAsync(id);
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

        /*****************************************************************/

        //private readonly ICafeService _cafeService;

        //public CafeController(ICafeService cafeService)
        //{
        //    _cafeService = cafeService;
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetCafeById(int id)
        //{
        //    try
        //    {
        //        var cafe = _cafeService.GetById(id);
        //        if (cafe == null)
        //        {
        //            var errorResponse = new ErrorResponse
        //            {
        //                Message = "El café no fue encontrado.",
        //                StatusCode = (int)HttpStatusCode.NotFound
        //            };
        //            return NotFound(errorResponse);
        //        }
        //        return Ok(cafe);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registra la excepción
        //        Console.WriteLine($"Error: {ex.Message}");

        //        var errorResponse = new ErrorResponse
        //        {
        //            Message = "Ocurrió un error interno en el servidor.",
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };

        //        return StatusCode(errorResponse.StatusCode, errorResponse);
        //    }
        //}

        //[HttpGet]
        //public IActionResult GetAllCafes()
        //{
        //    try
        //    {
        //        var cafes = _cafeService.GetAll();
        //        return Ok(cafes);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registra la excepción
        //        Console.WriteLine($"Error: {ex.Message}");

        //        var errorResponse = new ErrorResponse
        //        {
        //            Message = "Ocurrió un error interno en el servidor.",
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };

        //        return StatusCode(errorResponse.StatusCode, errorResponse);
        //    }
        //}

        //[HttpPost]
        //public IActionResult CreateCafe([FromBody] Cafe cafe)
        //{
        //    try
        //    {
        //        _cafeService.Create(cafe);
        //        return CreatedAtAction(nameof(GetCafeById), new { id = cafe.Id }, cafe);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registra la excepción
        //        Console.WriteLine($"Error: {ex.Message}");

        //        var errorResponse = new ErrorResponse
        //        {
        //            Message = "Ocurrió un error interno en el servidor.",
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };

        //        return StatusCode(errorResponse.StatusCode, errorResponse);
        //    }
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateCafe(int id, [FromBody] Cafe cafe)
        //{
        //    try
        //    {
        //        _cafeService.Update(id, cafe);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registra la excepción
        //        Console.WriteLine($"Error: {ex.Message}");

        //        var errorResponse = new ErrorResponse
        //        {
        //            Message = "Ocurrió un error interno en el servidor.",
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };

        //        return StatusCode(errorResponse.StatusCode, errorResponse);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteCafe(int id)
        //{
        //    try
        //    {
        //        _cafeService.Delete(id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registra la excepción
        //        Console.WriteLine($"Error: {ex.Message}");

        //        var errorResponse = new ErrorResponse
        //        {
        //            Message = "Ocurrió un error interno en el servidor.",
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };

        //        return StatusCode(errorResponse.StatusCode, errorResponse);
        //    }
        //}

    }
}
