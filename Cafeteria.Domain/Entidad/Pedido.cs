using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidad
{
    public enum EstadoPedido
    {
        Borrador,
        Pendiente,
        Procesando,
        PendienteFacturacion
    }

    public class Pedido
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; } // Propiedad de navegación a Comanda
        public int CafeId { get; set; }
        public Cafe Cafe { get; set; } // Propiedad de navegación a Cafe
        public int Cantidad { get; set; }
        public EstadoPedido Estado { get; set; }
        // Otras propiedades y métodos
    }
}
