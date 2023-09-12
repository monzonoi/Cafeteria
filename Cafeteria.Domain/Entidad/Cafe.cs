using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public class Cafe
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        // Otros atributos y propiedades

        public Cafe(string nombre, decimal precio)
        {
            Nombre = nombre;
            Precio = precio;
        }
    }
}
