using Cafeteria.API.Request;
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
        public async Task<ActionResult<int>> CrearUsuario([FromBody] CrearUsuarioRequest request)
        {
            var idCreado = await _usuarioService.CrearUsuarioAsync(request.UsuarioSolicitante, request.UsuarioNuevo);
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
        public async Task<IActionResult> EliminarUsuario(UsuarioDto administrador, int usuarioIdAEliminar)
        {            
            try
            {
                await _usuarioService.EliminarUsuarioAsync(administrador, usuarioIdAEliminar);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPost("RegistrarUsuarioAsync")]
        public async Task<IActionResult> RegistrarUsuarioAsync([FromBody] UsuarioDto usuarioDto)
        {
            try
            {                
                if (usuarioDto == null)
                {
                    return BadRequest("El objeto UsuarioDto es inválido.");
                }
                             
                var nuevoUsuario = await _usuarioService.RegistrarUsuarioAsync(usuarioDto);

                if (nuevoUsuario != null)
                {                    
                    return Created("usuario/ObtenerUsuarioPorId", nuevoUsuario);
                }
                else
                {                    
                    return BadRequest("No se pudo registrar al usuario.");
                }
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex.Message);
            }
        }


    }
}
