using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidades
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // Otras propiedades de rol

        // Relación con Usuario (Varios Usuarios -> 1 Rol)
        public List<Usuario> Usuarios { get; set; }
    }

}
