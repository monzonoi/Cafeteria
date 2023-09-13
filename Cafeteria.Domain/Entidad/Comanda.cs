using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidad
{
    public enum EstadoComanda
    {
        Pendiente = 1,
        EnProceso = 2,
        Finalizada = 3,
        Facturada = 4,
        Cancelada = 5
    }


    public class Comanda
    {  

        [Key] // Clave primaria
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; } // Clave foránea para el usuario

        public virtual Usuario Usuario { get; set; } // Propiedad de navegación al usuario

        public virtual ICollection<Pedido> Pedidos { get; set; } // Colección de pedidos en la comanda
        public Factura Factura { get; set; } // Relación maestro-detalle con Factura


        public EstadoComanda Estado { get; set; }

        // Constructor
        public Comanda()
        {
            FechaCreacion = DateTime.UtcNow;
            Pedidos = new List<Pedido>();
            Estado = EstadoComanda.Pendiente;
        }


        public void IniciarProceso()
        {
            if (Estado == EstadoComanda.Pendiente)
            {
                Estado = EstadoComanda.EnProceso;
            }
            else
            {
                throw new InvalidOperationException("La comanda no puede cambiar al estado 'En Proceso' desde su estado actual.");
            }
        }

        public void Finalizar()
        {
            if (Estado == EstadoComanda.EnProceso)
            {
                Estado = EstadoComanda.Finalizada;
            }
            else
            {
                throw new InvalidOperationException("La comanda no puede cambiar al estado 'Finalizada' desde su estado actual.");
            }
        }

        public void Facturar()
        {
            if (Estado == EstadoComanda.Finalizada)
            {
                Estado = EstadoComanda.Facturada;
            }
            else
            {
                throw new InvalidOperationException("La comanda no puede cambiar al estado 'Facturada' desde su estado actual.");
            }
        }

        public void Cancelar()
        {
            if (Estado == EstadoComanda.Pendiente || Estado == EstadoComanda.EnProceso)
            {
                Estado = EstadoComanda.Cancelada;
            }
            else
            {
                throw new InvalidOperationException("La comanda no puede cambiar al estado 'Cancelada' desde su estado actual.");
            }
        }
    }
}
