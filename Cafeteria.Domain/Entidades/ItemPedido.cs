using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidades
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TiempoRealizacion { get; set; }
        public decimal Precio { get; set; }
        // Otras propiedades de item

        // Relación con Pedido (FK)
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        // Relación con MateriaPrima (1 ItemPedido -> Varios MateriaPrima)
        public List<MateriaPrima> MateriaPrima { get; set; }
    }

}
