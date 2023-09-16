using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Cafeteria.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Tests.Mock
{
    public class ComandaServiceMock
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly Mock<IMateriaPrimaService> _materiaPrimaServiceMock;

        public readonly List<ComandaDto> _comandasEnMemoria;
        public readonly List<PedidoDto> _pedidosEnMemoria;



        public ComandaServiceMock(IComandaRepository _comandaRepository, Mock<IUsuarioService> usuarioServiceMock, Mock<IMateriaPrimaService> materiaPrimaServiceMock)
        {
            _usuarioServiceMock = usuarioServiceMock ?? throw new ArgumentNullException(nameof(usuarioServiceMock));
            _materiaPrimaServiceMock = materiaPrimaServiceMock ?? throw new ArgumentNullException(nameof(materiaPrimaServiceMock));

        }

        public ComandaServiceMock(List<ComandaDto> comandas, List<PedidoDto> pedidos)
        {
            _comandasEnMemoria = comandas;
            _pedidosEnMemoria = pedidos;
        }

        public async Task CambiarEstadoPedidoAsync(PedidoDto pedido, string nuevoEstado)
        {
            // Buscar el pedido en la lista de pedidos en memoria
            var pedidoEnMemoria = _pedidosEnMemoria.FirstOrDefault(p => p.Id == pedido.Id);

            var estadosTerminados = new string[] { "Terminado", "Cancelado" };

            if (pedidoEnMemoria != null)
            {
                // Cambiar el estado del pedido
                pedidoEnMemoria.Estado = nuevoEstado;

                // Verificar si todos los pedidos de la comanda están en estado "Finalizado"
                var todosPedidosFinalizados = _pedidosEnMemoria
                    .Where(p => p.ComandaId == pedidoEnMemoria.ComandaId)
                    .All(p => estadosTerminados.Contains(p.Estado));

                if (todosPedidosFinalizados)
                {
                    // Obtener la comanda asociada a estos pedidos
                    var comandaEnMemoria = _comandasEnMemoria.FirstOrDefault(c => c.Id == pedidoEnMemoria.ComandaId);

                    if (comandaEnMemoria != null)
                    {
                        // Cambiar el estado de la comanda a "PendienteFacturacion"
                        comandaEnMemoria.Estado = "PendienteFacturacion";
                    }
                }
            }
        }


    }
}
