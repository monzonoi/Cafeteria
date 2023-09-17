using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Tests.Mock
{
    public class PedidoServiceMock : IPedidoService
    {
        private readonly List<PedidoDto> _pedidos;

        public PedidoServiceMock()
        {
            // Inicializa una lista simulada de pedidos
            _pedidos = new List<PedidoDto>
        {
            new PedidoDto { Id = 1, Estado = "Pendiente" },
            new PedidoDto { Id = 2, Estado = "EnProceso" },
            new PedidoDto { Id = 3, Estado = "Completado" }
            // Agrega más pedidos según tus necesidades
        };
        }

        public Task ActualizarPedidoAsync(int pedidoId, PedidoDto pedidoDto)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDto> CambiarEstadoPedidoAsync(int pedidoId, string nuevoEstado)
        {
            throw new NotImplementedException();
        }

        public Task<int> CrearPedidoAsync(PedidoDto pedidoDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarPedidoAsync(UsuarioDto usuario, PedidoDto pedido)
        {
            // Simula la lógica de edición de un pedido
            var pedidoExistente = _pedidos.FirstOrDefault(p => p.Id == pedido.Id);

            if (pedidoExistente != null)
            {
                pedidoExistente.Estado = pedido.Estado;
                return Task.FromResult(true); // Simulación exitosa
            }

            return Task.FromResult(false); // Simulación de error (pedido no encontrado)
        }

        public Task EliminarPedidoAsync(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDto> ObtenerPedidoPorIdAsync(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDto>> ObtenerTodosLosPedidosAsync()
        {
            throw new NotImplementedException();
        }
    }

}
