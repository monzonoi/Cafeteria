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
        public int FacturaId { get; set; }
        public Factura Factura { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        // Otras propiedades y métodos
    }

}
