using Cafeteria.Application.Enum;

namespace Cafeteria.Application.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<ItemPedidoDto> Items { get; set; }
        public EstadoPedido Estado { get; set; }
        public int UsuarioId { get; set; }
    }
}
