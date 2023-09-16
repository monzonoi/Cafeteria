using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        // Otras propiedades de pedido

        // Relación con Usuario (FK)
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relación con Comanda (FK)
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }

        // Relación con ItemPedido (1 Pedido -> Varios ItemPedido)
        public List<ItemPedido> Items { get; set; }
        public string Estado { get; set; } //Pendiente, EnProceso, Completado, Cancelado
    }

}
