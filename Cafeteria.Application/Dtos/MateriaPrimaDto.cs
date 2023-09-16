using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Application.Dtos
{
    public class MateriaPrimaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadDisponible { get; set; }        

        public int ItemPedidoId { get; set; } // Identificador del ítem de pedido relacionado
        public ItemPedidoDto ItemPedido { get; set; } // Objeto ItemPedidoDto relacionado
        public decimal Cantidad { get; set; }
    }

}
