using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidades
{
    public class Comanda
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        // Otras propiedades de comanda

        // Relación con Usuario (FK)
        public int EmpleadoId { get; set; }
        public Usuario Empleado { get; set; }

        // Relación con Pedido (1 Comanda -> Varios Pedidos)
        public List<Pedido> Pedidos { get; set; }
        public string Estado { get; set; } //EnProceso, Completada, Cancelada, Facturada
        
    }
}
