

using Cafeteria.Domain.Entidades;

namespace Cafeteria.Application.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<ItemPedidoDto> Items { get; set; }
        public string Estado { get; set; }
        // Relación con Usuario (FK)
        public int UsuarioId { get; set; }
        public UsuarioDto Usuario { get; set; }

        // Relación con Comanda (FK)
        public int ComandaId { get; set; }
        public ComandaDto Comanda { get; set; } // Agregar esta propiedad para vincular con ComandaDto

    }
}
