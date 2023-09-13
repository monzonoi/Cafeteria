using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidad
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; } // Ejemplo: "Usuario", "Empleado", "Supervisor", "Administrador"
                                        // Otras propiedades si es necesario
    }
}
