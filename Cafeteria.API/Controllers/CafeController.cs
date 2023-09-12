using Microsoft.AspNetCore.Mvc;
using Cafeteria.Application; // Asegúrate de importar el espacio de nombres de la aplicación
using Cafeteria.Application.Service;
using Cafeteria.Domain;

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
        public IActionResult GetAllCafes()
        {
            var cafes = _cafeService.GetAll();
            return Ok(cafes);
        }

        [HttpGet("{id}")]
        public IActionResult GetCafeById(int id)
        {
            var cafe = _cafeService.GetById(id);
            if (cafe == null)
            {
                return NotFound();
            }
            return Ok(cafe);
        }

        [HttpPost]
        public IActionResult CreateCafe([FromBody] Cafe cafe)
        {
            _cafeService.Create(cafe);
            return CreatedAtAction(nameof(GetCafeById), new { id = cafe.Id }, cafe);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCafe(int id, [FromBody] Cafe cafe)
        {
            _cafeService.Update(id, cafe);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCafe(int id)
        {
            _cafeService.Delete(id);
            return NoContent();
        }
    }
}
