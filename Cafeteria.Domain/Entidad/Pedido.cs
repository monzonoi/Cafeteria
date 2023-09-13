using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidad
{

    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; } // Clave externa para el usuario que realizó el pedido
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        public int ComandaId { get; set; } // Clave externa para la comanda asociada

        // Propiedad de navegación para el usuario
        public Usuario Usuario { get; set; }

        // Propiedad de navegación para la comanda
        public Comanda Comanda { get; set; }

        // Colección de ítems de pedido
        public List<ItemPedido> Items { get; set; }

        // Otras propiedades y métodos si es necesario
    }
}
