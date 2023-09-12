using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public class Cafe
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }
        // Otros atributos y propiedades

        public Cafe(string nombre, decimal precio)
        {
            Nombre = nombre;
            Precio = precio;
        }
    }
}
