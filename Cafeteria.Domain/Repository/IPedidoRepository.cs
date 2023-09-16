using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public interface IPedidoRepository
    {
        Task<Pedido> ObtenerPorIdAsync(int pedidoId);
        Task<IEnumerable<Pedido>> ObtenerTodosAsync();
        Task AgregarAsync(Pedido pedido);
        Task ActualizarAsync(Pedido pedido);
        Task EliminarAsync(int pedidoId);
    }

}
