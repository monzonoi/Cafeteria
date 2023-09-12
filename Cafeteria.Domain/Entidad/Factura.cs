using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public class Factura
    {
        public int Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Numero { get; private set; }
        public List<DetalleFactura> Detalles { get; private set; }

        // Constructor privado para la creación de nuevas facturas
        private Factura() { }

        public Factura(DateTime fecha, string numero)
        {
            Fecha = fecha;
            Numero = numero;
            Detalles = new List<DetalleFactura>();
        }

        public void AgregarDetalle(string descripcion, decimal precio, int cantidad)
        {
            // Realiza las validaciones necesarias antes de agregar un detalle
            Detalles.Add(new DetalleFactura(descripcion, precio, cantidad));
        }
    }
}
