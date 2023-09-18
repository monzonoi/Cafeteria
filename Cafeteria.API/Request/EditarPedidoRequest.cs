using Cafeteria.Application.Dtos;

namespace Cafeteria.API.Request
{
    public class EditarPedidoRequest
    {
        public UsuarioDto Usuario { get; set; }
        public PedidoDto Pedido { get; set; }
    }
}
