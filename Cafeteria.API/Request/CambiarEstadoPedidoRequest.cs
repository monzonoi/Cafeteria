using Cafeteria.Application.Dtos;

namespace Cafeteria.API.Request
{
    public class CambiarEstadoPedidoRequest
    {
        public PedidoDto PedidoDto { get; set; }
        public string NuevoEstado { get; set; }
    }

}
