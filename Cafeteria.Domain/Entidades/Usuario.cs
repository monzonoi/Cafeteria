using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // Otras propiedades de usuario

        // Relación con Rol (FK)
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        // Relación con Pedido (1 Usuario -> Varios Pedidos)
        public List<Pedido> Pedidos { get; set; }
    }
}
