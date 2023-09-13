using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public class Factura
    {
        public int Id { get; set; }
        public int PedidoId { get; set; } // Clave externa para el pedido asociado
        public DateTime FechaFacturacion { get; set; }
        public decimal Total { get; set; }

        // Propiedad de navegación para el pedido asociado
        public Pedido Pedido { get; set; }

        // Colección de detalles de factura
        public List<DetalleFactura> Detalles { get; set; }

        // Otras propiedades y métodos si es necesario
    }

}
