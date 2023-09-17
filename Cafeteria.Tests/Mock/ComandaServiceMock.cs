using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;
using Cafeteria.Domain;
using Cafeteria.Domain.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Tests.Mock
{
    public class ComandaServiceMock : IComandaService
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

        public ComandaServiceMock(List<ComandaDto> comandas)
        {
            _comandasEnMemoria = comandas;
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

        public async Task<List<PedidoDto>> ObtenerTodosAsync(UsuarioDto usuario)
        {
            // Verificar si el usuario tiene permiso para ver los pedidos
            if (usuario.Rol.Nombre == "Supervisor" || usuario.Rol.Nombre == "Administrador")
            {
                // Filtrar las comandas según el usuario (Supervisor/Administrador ve todas las comandas)
                var comandasFiltradas = _comandasEnMemoria;

                // Crear una lista para almacenar todos los pedidos de las comandas
                var todosLosPedidos = new List<PedidoDto>();

                // Iterar a través de las comandas filtradas
                foreach (var comanda in comandasFiltradas)
                {
                    // Agregar todos los pedidos de la comanda a la lista de pedidos
                    todosLosPedidos.AddRange(comanda.Pedidos);
                }

                return todosLosPedidos;
            }
            else
            {
                // El usuario no tiene permiso para ver los pedidos
                throw new UnauthorizedAccessException("El usuario no tiene permiso para ver los pedidos.");
            }
        }

        public Task CambiarEstadoComandaAsync(int comandaId, string nuevoEstado)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComandaDto>> ObtenerTodasLasComandasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> ObtenerComandaPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CrearComandaAsync(ComandaDto comandaDto)
        {
            throw new NotImplementedException();
        }

        public Task ActualizarComandaAsync(int id, ComandaDto comandaDto)
        {
            throw new NotImplementedException();
        }

        public Task EliminarComandaAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> ObtenerComandaAsync(int comandaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComandaDto>> ObtenerComandasPorUsuarioAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDto>> ObtenerPedidosPorComandaAsync(int comandaId)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> AgregarPedidoAsync(int comandaId, PedidoDto pedido, UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> CambiarEstadoComandaAsync(int comandaId, string nuevoEstado, UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> FacturarComandaAsync(int comandaId, UsuarioDto supervisor)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PedidoDto>> ObtenerTrabajosRealizadosAsync(UsuarioDto usuario)
        {
            // Verificamos si el usuario tiene el rol de "Supervisor" o "Administrador"
            if (usuario.Rol.Nombre != "Supervisor" && usuario.Rol.Nombre != "Administrador")
            {
                // Si no tiene los roles permitidos, lanzamos una excepción o devolvemos una lista vacía, según tu preferencia
                throw new UnauthorizedAccessException("No tienes permisos para ver los trabajos realizados por empleados.");
            }

            // Simulamos la obtención de todas las comandas no completadas
            var comandasCompletadas = _comandasEnMemoria.Where(c => c.Estado != "Completada").ToList();

            // Creamos una lista para almacenar los trabajos realizados por los empleados
            var trabajosRealizados = new List<PedidoDto>();

            // Iteramos sobre todas las comandas completadas
            foreach (var comanda in comandasCompletadas)
            {
                // Filtramos los pedidos que están en estado no "Completado" y pertenecen a un usuario con rol "Empleado"
                var pedidosEmpleados = comanda.Pedidos.Where(p => p.Usuario.Rol.Nombre == "Empleado" && p.Estado != "Completado").ToList();

                // Agregamos los pedidos de empleados a la lista de trabajos realizados
                trabajosRealizados.AddRange(pedidosEmpleados);
            }

            return await Task.FromResult(trabajosRealizados);
        }

        public Task<bool> EditarComandaAsync(UsuarioDto usuario, ComandaDto comanda)
        {
            // Simula la lógica de edición de una comanda
            // Por ejemplo, puedes verificar si el usuario tiene permiso para editar
            // y luego actualizar la comanda en la lista simulada

            var comandaExistente = _comandasEnMemoria.FirstOrDefault(c => c.Id == comanda.Id);

            if (comandaExistente != null)
            {
                comandaExistente.Estado = comanda.Estado;
                return Task.FromResult(true); // Simulación exitosa
            }

            return Task.FromResult(false); // Simulación de error (comanda no encontrada)
        }

    }
}
