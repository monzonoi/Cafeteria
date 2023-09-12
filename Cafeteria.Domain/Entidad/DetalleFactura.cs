using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public class DetalleFactura
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Precio { get; private set; }
        public int Cantidad { get; private set; }

        // Constructor privado para Entity Framework Core
        private DetalleFactura() { }

        public DetalleFactura(string descripcion, decimal precio, int cantidad)
        {
            Descripcion = descripcion;
            Precio = precio;
            Cantidad = cantidad;
        }
    }
}
