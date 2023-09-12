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

        // Implementa acciones para interactuar con ICafeService

        // Endpoint para obtener todos los cafés
        [HttpGet(Name = "GetAllCafes")]        
        public ActionResult<IEnumerable<Cafe>> GetAllCafes()
        {
            var cafes = _cafeService.GetAll();
            return Ok(cafes);
        }


        //// Endpoint para obtener todos los cafés
        //[HttpGet(Name = "GetById")]
        //public ActionResult<Cafe> GetById()
        //{
        //    var cafes = _cafeService.GetAll().FirstOrDefault();
        //    return Ok(cafes);
        //}
    }
}
