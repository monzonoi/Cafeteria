using Cafeteria.Application.Dtos;

namespace Cafeteria.API.Request
{
    public class EditarComandaRequest
    {
        public ComandaDto ComandaDto { get; set; }
        public UsuarioDto Usuario { get; set; }
    }

}
