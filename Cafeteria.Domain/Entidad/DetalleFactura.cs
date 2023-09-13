using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; } // Clave externa para la factura a la que pertenece este detalle
        public int CafeId { get; set; } // Clave externa para el café vendido
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Propiedad de navegación para la factura a la que pertenece este detalle
        public Factura Factura { get; set; }

        // Propiedad de navegación para el café vendido
        public Cafe Cafe { get; set; }

        // Otras propiedades y métodos si es necesario
    }
}
