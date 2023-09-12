using Microsoft.AspNetCore.Mvc;
using Cafeteria.Application; // Asegúrate de importar el espacio de nombres de la aplicación
using Cafeteria.Application.Service;
using Cafeteria.Domain;
using System.Net;

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

        [HttpGet("{id}")]
        public IActionResult GetCafeById(int id)
        {
            try
            {
                var cafe = _cafeService.GetById(id);
                if (cafe == null)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Message = "El café no fue encontrado.",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                    return NotFound(errorResponse);
                }
                return Ok(cafe);
            }
            catch (Exception ex)
            {
                // Registra la excepción
                Console.WriteLine($"Error: {ex.Message}");

                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error interno en el servidor.",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }

        [HttpGet]
        public IActionResult GetAllCafes()
        {
            try
            {
                var cafes = _cafeService.GetAll();
                return Ok(cafes);
            }
            catch (Exception ex)
            {
                // Registra la excepción
                Console.WriteLine($"Error: {ex.Message}");

                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error interno en el servidor.",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }

        [HttpPost]
        public IActionResult CreateCafe([FromBody] Cafe cafe)
        {
            try
            {
                _cafeService.Create(cafe);
                return CreatedAtAction(nameof(GetCafeById), new { id = cafe.Id }, cafe);
            }
            catch (Exception ex)
            {
                // Registra la excepción
                Console.WriteLine($"Error: {ex.Message}");

                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error interno en el servidor.",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCafe(int id, [FromBody] Cafe cafe)
        {
            try
            {
                _cafeService.Update(id, cafe);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Registra la excepción
                Console.WriteLine($"Error: {ex.Message}");

                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error interno en el servidor.",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCafe(int id)
        {
            try
            {
                _cafeService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Registra la excepción
                Console.WriteLine($"Error: {ex.Message}");

                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error interno en el servidor.",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }

    }
}
