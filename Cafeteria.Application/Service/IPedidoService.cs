using Cafeteria.Application.Dtos;
using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDto>> ObtenerTodosLosPedidosAsync();
        Task<PedidoDto> ObtenerPedidoPorIdAsync(int pedidoId);
        Task<int> CrearPedidoAsync(PedidoDto pedidoDto);
        Task ActualizarPedidoAsync(int pedidoId, PedidoDto pedidoDto);
        Task EliminarPedidoAsync(int pedidoId);
    }

}
