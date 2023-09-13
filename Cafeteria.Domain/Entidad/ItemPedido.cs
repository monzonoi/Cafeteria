using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidad
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; } // Clave externa para el pedido al que pertenece este ítem
        public int CafeId { get; set; } // Clave externa para el café que se ha pedido
        public int Cantidad { get; set; }

        // Propiedad de navegación para el pedido al que pertenece este ítem
        public Pedido Pedido { get; set; }

        // Propiedad de navegación para el café que se ha pedido
        public Cafe Cafe { get; set; }

        // Otras propiedades si es necesario
    }
}
