using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidades
{
    public class MateriaPrima
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // Otras propiedades de registro

        // Relación con ItemPedido (FK)
        public int ItemPedidoId { get; set; }
        public ItemPedido ItemPedido { get; set; }
    }
}
