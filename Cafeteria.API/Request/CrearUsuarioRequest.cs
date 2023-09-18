using Cafeteria.Application.Dtos;

namespace Cafeteria.API.Request
{
    public class CrearUsuarioRequest
    {
        public UsuarioDto UsuarioSolicitante { get; set; }
        public UsuarioDto UsuarioNuevo { get; set; }
    }

}
