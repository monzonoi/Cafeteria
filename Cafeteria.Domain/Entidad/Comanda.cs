using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidad
{
    public class Comanda
    {
        public int Id { get; set; }
        public int PedidoId { get; set; } // Clave externa para el pedido asociado
        public DateTime FechaCreacion { get; set; }
        // Otras propiedades si es necesario
    }
}
