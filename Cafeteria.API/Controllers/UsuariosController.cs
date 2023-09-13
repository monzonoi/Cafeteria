using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Cafeteria.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObtenerTodosLosUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerTodosLosUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearUsuario([FromBody] UsuarioDto usuarioDto)
        {
            var idCreado = await _usuarioService.CrearUsuarioAsync(usuarioDto);
            return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = idCreado }, idCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                await _usuarioService.ActualizarUsuarioAsync(id, usuarioDto);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                await _usuarioService.EliminarUsuarioAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
